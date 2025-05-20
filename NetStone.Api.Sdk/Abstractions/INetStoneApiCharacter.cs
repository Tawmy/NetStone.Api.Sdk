using NetStone.Common.DTOs.Character;
using NetStone.Common.Enums;
using NetStone.Common.Queries;
using Refit;

namespace NetStone.Api.Sdk.Abstractions;

[Headers("Authorization: Bearer", "X-API-Version: 3")]
public interface INetStoneApiCharacter
{
    private const string Base = "/Character";

    [Post($"{Base}/Search")]
    Task<CharacterSearchPageDto> SearchAsync(CharacterSearchQuery query, short page = 1,
        CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}")]
    Task<CharacterDto> GetAsync(string lodestoneId, int? maxAge = null, FallbackType useFallback = FallbackType.None,
        CancellationToken cancellationToken = default);

    [Get($"{Base}/ByName/{{name}}/{{world}}")]
    Task<CharacterDto> GetByNameAsync(string name, string world, CancellationToken cancellationToken = default);

    [Get($"{Base}/ClassJobs/{{lodestoneId}}")]
    Task<CharacterClassJobOuterDto> GetClassJobsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/Minions/{{lodestoneId}}")]
    Task<CollectionDto<CharacterMinionDto>> GetMinionsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/Mounts/{{lodestoneId}}")]
    Task<CollectionDto<CharacterMountDto>> GetMountsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/Achievements/{{lodestoneId}}")]
    Task<CharacterAchievementOuterDto> GetAchievementsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);
}