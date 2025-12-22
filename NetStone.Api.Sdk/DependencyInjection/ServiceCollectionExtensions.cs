using System.Net;
using AspNetCoreExtensions.Keycloak;
using AspNetCoreExtensions.Keycloak.Options;
using Duende.AccessTokenManagement;
using Microsoft.Extensions.DependencyInjection;
using NetStone.Api.Sdk.Abstractions;
using NetStone.Api.Sdk.Refit;
using Refit;
using NotFoundException = NetStone.Common.Exceptions.NotFoundException;

namespace NetStone.Api.Sdk.DependencyInjection;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddNetStoneApi(NetStoneApiOptions options)
        {
            var kc = KeycloakConfiguration.WithSignedJwt(options.AuthAuthority.ToString(), options.AuthClientId,
                options.CertificatePath, options.PrivateKeyPath, options.AuthScopes);

            services.AddKeycloakClientCredentials(kc);
            services.AddConfiguredRefitClient<INetStoneApiCharacter>(options);
            services.AddConfiguredRefitClient<INetStoneApiFreeCompany>(options);
        }

        private void AddConfiguredRefitClient<T>(NetStoneApiOptions options)
            where T : class
        {
            services.AddRefitClient<T>(_ => new RefitSettings
                {
                    UrlParameterFormatter = new RefitFlagEnumFormatter(),
                    ExceptionFactory = async response =>
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return null;
                        }

                        if (response.StatusCode is HttpStatusCode.NotFound)
                        {
                            return new NotFoundException();
                        }

                        var content = await response.Content.ReadAsStringAsync();
                        var msg = $"{response.StatusCode} • {response.ReasonPhrase} • {content}";
                        return new NetStoneException(msg, response.StatusCode, response.ReasonPhrase, response.Headers,
                            response.RequestMessage!.Method, response.RequestMessage, response.Content.Headers,
                            content);
                    }
                })
                .ConfigureHttpClient(x => x.BaseAddress = options.ApiBaseAddress)
                .AddClientCredentialsTokenHandler(ClientCredentialsClientName.Parse(options.AuthClientId))
                .AddStandardResilienceHandler(x =>
                {
                    x.AttemptTimeout.Timeout = TimeSpan.FromSeconds(options.RequestTimeout);
                    x.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(options.RequestTimeout * 3);
                    x.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(options.RequestTimeout * 3);
                });
        }
    }
}