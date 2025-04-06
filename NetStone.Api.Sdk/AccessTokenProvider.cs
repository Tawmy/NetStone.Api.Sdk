using Microsoft.Identity.Client;

namespace NetStone.Api.Sdk;

internal class AccessTokenProvider(NetStoneApiOptions options)
{
    private static readonly SemaphoreSlim AuthenticationLock = new(1, 1);
    private AuthenticationResult? _authenticationResult;

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (_authenticationResult is not null)
        {
            // access token has been retrieved before
            if (_authenticationResult.ExpiresOn > DateTimeOffset.UtcNow.AddMinutes(1))
            {
                // expires in more than one minute (we'd want to refresh if valid for less than that)
                return _authenticationResult.AccessToken;
            }
        }

        // prevent parallel requests from requesting new access token at the same time
        await AuthenticationLock.WaitAsync(cancellationToken);

        try
        {
            await RetrieveNewAccessTokenAsync(cancellationToken);
        }
        finally
        {
            AuthenticationLock.Release();
        }

        if (_authenticationResult?.AccessToken is null)
        {
            throw new InvalidOperationException(
                "Access token still null after retrieving new one. Please check config.");
        }

        return _authenticationResult.AccessToken;
    }

    private async Task RetrieveNewAccessTokenAsync(CancellationToken cancellationToken)
    {
        var app = ConfidentialClientApplicationBuilder
            .Create(options.AuthClientId)
            .WithClientSecret(options.AuthClientSecret)
            .WithOidcAuthority(options.AuthAuthority.ToString())
            .Build();

        _authenticationResult = await app.AcquireTokenForClient(options.AuthScopes).ExecuteAsync(cancellationToken);
    }
}