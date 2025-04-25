using System.Net;
using Microsoft.Extensions.DependencyInjection;
using NetStone.Api.Sdk.Abstractions;
using Refit;
using NotFoundException = NetStone.Common.Exceptions.NotFoundException;

namespace NetStone.Api.Sdk.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddNetStoneApi(this IServiceCollection services, NetStoneApiOptions options)
    {
        services.AddSingleton<AccessTokenProvider>(_ => new AccessTokenProvider(options));

        services.AddConfiguredRefitClient<INetStoneApiCharacter>(options.ApiBaseAddress);
        services.AddConfiguredRefitClient<INetStoneApiFreeCompany>(options.ApiBaseAddress);
    }

    private static void AddConfiguredRefitClient<T>(this IServiceCollection services, Uri baseAddress) where T : class
    {
        services.AddRefitClient<T>(x => new RefitSettings
            {
                AuthorizationHeaderValueGetter = async (_, cancellationToken) =>
                    await x.GetRequiredService<AccessTokenProvider>().GetAccessTokenAsync(cancellationToken),
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
                    return new NetStoneException(msg);
                }
            })
            .ConfigureHttpClient(x => x.BaseAddress = baseAddress)
            .AddStandardResilienceHandler(x =>
            {
                x.AttemptTimeout.Timeout = TimeSpan.FromSeconds(60);
                x.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(180);
                x.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(180);
            });
    }
}