namespace NetStone.Api.Sdk;

/// <summary>
///     Configuration for NetStone API.
/// </summary>
/// <param name="ApiBaseAddress">The base address of the NetStone API the client will connect to.</param>
/// <param name="AuthAuthority">OAuth Authority URL, used to retrieve OAuth metadata.</param>
/// <param name="AuthClientId">OAuth client ID.</param>
/// <param name="AuthClientSecret">OAuth client secret.</param>
/// <param name="AuthScopes">Authorization scopes to be submitted with request.</param>
/// <param name="RequestTimeout">Timeout for requests in seconds.</param>
public record NetStoneApiOptions(
    Uri ApiBaseAddress,
    Uri AuthAuthority,
    string AuthClientId,
    string AuthClientSecret,
    string[] AuthScopes,
    int RequestTimeout = 60);