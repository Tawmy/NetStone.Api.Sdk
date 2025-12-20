namespace NetStone.Api.Sdk;

/// <summary>
///     Configuration for NetStone API.
/// </summary>
public record NetStoneApiOptions
{
    /// <summary>The base address of the NetStone API the client will connect to.</summary>
    public required Uri ApiBaseAddress { get; init; }

    /// <summary>OAuth Authority URL, used to retrieve OAuth metadata.</summary>
    public required Uri AuthAuthority { get; init; }

    /// <summary>OAuth client ID.</summary>
    public required string AuthClientId { get; init; }

    /// <summary>
    ///     Path to certificate for signed JWT client authentication.
    /// </summary>
    public required string CertificatePath { get; init; }

    /// <summary>
    ///     Path to private key for signed JWT client authentication.
    /// </summary>
    public required string PrivateKeyPath { get; init; }

    /// <summary>Authorization scopes to be submitted with request.</summary>
    public string[] AuthScopes { get; init; } = [];

    /// <summary>Timeout for requests in seconds.</summary>
    public int RequestTimeout { get; init; } = 60;
}