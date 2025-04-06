using Microsoft.Extensions.DependencyInjection;
using NetStone.Api.Sdk.Abstractions;
using Refit;

namespace NetStone.Api.Sdk.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddNetStoneApiClient(this IServiceCollection services, NetStoneApiOptions options)
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
                    await x.GetRequiredService<AccessTokenProvider>().GetAccessTokenAsync(cancellationToken)
            })
            .ConfigureHttpClient(x => x.BaseAddress = baseAddress)
            .AddStandardResilienceHandler();
    }
}