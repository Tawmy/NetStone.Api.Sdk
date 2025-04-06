namespace NetStone.Api.Sdk.Test;

public static class EnvironmentVariables
{
    /// <summary>
    ///     Root URI for the NetStone API.
    /// </summary>
    /// <remarks>
    ///     Routes will be appended to this URI.
    /// </remarks>
    public const string ApiBaseAddress = "API_BASE_ADDRESS";

    /// <summary>
    ///     Authority for OAuth Client Credentials flow.
    /// </summary>
    public const string AuthAuthority = "AUTH_AUTHORITY";

    /// <summary>
    ///     OAuth 2.0 client ID.
    /// </summary>
    public const string AuthClientId = "AUTH_CLIENT_ID";

    /// <summary>
    ///     OAuth 2.0 client scopes.
    /// </summary>
    /// <remarks>
    ///     Scopes are divided with spaces.
    /// </remarks>
    public const string AuthScopes = "AUTH_SCOPES";

    /// <summary>
    ///     OAuth 2.0 client secret.
    /// </summary>
    public const string AuthClientSecret = "AUTH_CLIENT_SECRET";
}