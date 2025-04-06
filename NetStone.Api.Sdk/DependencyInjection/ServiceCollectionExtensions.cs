using Microsoft.Extensions.DependencyInjection;
using NetStone.Api.Sdk.Abstractions;
using Refit;

namespace NetStone.Api.Sdk.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddNetStoneApiClient(this IServiceCollection services, NetStoneApiOptions options)
    {
        services.AddSingleton<AccessTokenProvider>(_ => new AccessTokenProvider(options));

        services.AddRefitClient<INetStoneApiCharacter>(ConfigureAuthorizationHeader)
            .ConfigureHttpClient(ConfigureBaseAddress);

        services.AddRefitClient<INetStoneApiFreeCompany>(ConfigureAuthorizationHeader)
            .ConfigureHttpClient(ConfigureBaseAddress);

        return;

        RefitSettings? ConfigureAuthorizationHeader(IServiceProvider x)
        {
            return new RefitSettings
            {
                AuthorizationHeaderValueGetter = async (_, cancellationToken) =>
                    await x.GetRequiredService<AccessTokenProvider>().GetAccessTokenAsync(cancellationToken)
            };
        }

        void ConfigureBaseAddress(HttpClient x)
        {
            x.BaseAddress = options.ApiBaseAddress;
        }
    }
}