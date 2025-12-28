# NetStone API SDK

SDK for NetStone API. Built for use with dependency injection in ASP.NET Core.

## How to Use

### Register SDK on startup

To access the API in your endpoints / controllers / other services, register it in your `Program.cs` file as follows:

```C#
var builder = WebApplication.CreateBuilder(args);

// other setup

builder.Services.AddNetStoneApi(options);

// other setup

var app = builder.Build(); // must be added before this line
```

You must pass an instance of `NetStoneApiOptions` to `AddNetStoneApi`.
However you initialise this instance is up to you, but please do store this configuration safely.

### NetStoneApiOptions

NetStone API uses OAuth 2.0 Client Credentials for authorization.

| Parameter           | Explanation                                                      | Example                                     |
|---------------------|------------------------------------------------------------------|---------------------------------------------|
| ApiBaseAddress      | The base address of the NetStone API the client will connect to. | https://netstone.api.tawmy.net              |
| AuthAuthority       | OAuth Authority URL, used to retrieve OAuth metadata.            | https://mydomain.net/realms/myKeycloakRealm |
| AuthClientId        | OAuth client ID.                                                 | my-client-id                                |
| AuthCertificatePath | OAuth signed JWT certificate path.                               | /mnt/cert/private.key                       |
| AuthPrivateKeyPath  | OAuth signed JWT private key path.                               | /mnt/cert/certificate.pem                   |
| AuthScopes          | Authorization scopes to be submitted with request.               | netstone.api (optional)                     |

### Retrieving Data

```C#
public class CharacterService(INetStoneApiCharacter apiCharacter) // Dependency injection
{
    public async Task GetCharacterAsync(string characterLodestoneId,
        CancellationToken cancellationToken = default)
    {
        CharacterDto character;
        try
        {
            character = await apiCharacter.GetAsync(characterLodestoneId,
                cancellationToken: cancellationToken);
        }
        catch (ApiException)
        {
            // handle exception
        }
        
        // do stuff with the retrieved character here
    }
}
```

Passing a cancellation token is not mandatory, but recommended.
Certain operations can take a long time depending on request complexity and what the Lodestone feels like today.

### Available Interfaces

- INetStoneApiCharacter
- INetStoneApiFreeCompany