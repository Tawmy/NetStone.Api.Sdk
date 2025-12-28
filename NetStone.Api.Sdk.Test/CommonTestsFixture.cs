using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStone.Api.Sdk.DependencyInjection;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace NetStone.Api.Sdk.Test;

public class CommonTestsFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        var apiBaseAddress = new Uri(Environment.GetEnvironmentVariable(EnvironmentVariables.ApiBaseAddress) ??
                                     throw new ArgumentNullException(EnvironmentVariables.ApiBaseAddress));
        var authAuthority = new Uri(Environment.GetEnvironmentVariable(EnvironmentVariables.AuthAuthority) ??
                                    throw new ArgumentNullException(EnvironmentVariables.AuthAuthority));
        var authClientId = Environment.GetEnvironmentVariable(EnvironmentVariables.AuthClientId) ??
                           throw new ArgumentNullException(EnvironmentVariables.AuthClientId);
        var authClientCert = Environment.GetEnvironmentVariable(EnvironmentVariables.AuthClientSignedJwtCertificate) ??
                             throw new ArgumentNullException(EnvironmentVariables.AuthClientSignedJwtCertificate);
        var authClientKey = Environment.GetEnvironmentVariable(EnvironmentVariables.AuthClientSignedJwtPrivateKey) ??
                            throw new ArgumentNullException(EnvironmentVariables.AuthClientSignedJwtPrivateKey);
        var authScopes = Environment.GetEnvironmentVariable(EnvironmentVariables.AuthScopes) ?? string.Empty;
        var authScopesArray = authScopes.Split(" ",
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var options = new NetStoneApiOptions
        {
            ApiBaseAddress = apiBaseAddress,
            AuthAuthority = authAuthority,
            AuthClientId = authClientId,
            AuthCertificatePath = authClientCert,
            AuthPrivateKeyPath = authClientKey,
            AuthScopes = authScopesArray
        };

        services.AddNetStoneApi(options);
    }

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new TestAppSettings();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        return ValueTask.CompletedTask;
    }
}