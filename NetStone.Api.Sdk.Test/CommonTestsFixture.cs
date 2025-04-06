using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStone.Api.Sdk.DependencyInjection;
using NetStone.Common.Helpers;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace NetStone.Api.Sdk.Test;

public class CommonTestsFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        var apiBaseAddress = EnvironmentVariableHelper.Get<Uri>(EnvironmentVariables.ApiBaseAddress);
        var authAuthority = EnvironmentVariableHelper.Get<Uri>(EnvironmentVariables.AuthAuthority);
        var authClientId = EnvironmentVariableHelper.Get(EnvironmentVariables.AuthClientId);
        var authClientSecret = EnvironmentVariableHelper.Get(EnvironmentVariables.AuthClientSecret);
        var authScopes = EnvironmentVariableHelper.Get(EnvironmentVariables.AuthScopes);
        var authScopesArray = authScopes.Split(" ",
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var options = new NetStoneApiOptions(apiBaseAddress, authAuthority, authClientId, authClientSecret,
            authScopesArray);

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