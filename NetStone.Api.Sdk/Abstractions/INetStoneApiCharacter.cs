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
    Task<CharacterDtoV3> GetAsync(string lodestoneId, int? maxAge = null, FallbackType useFallback = FallbackType.None,
        CancellationToken cancellationToken = default);

    [Get($"{Base}/ClassJobs/{{lodestoneId}}")]
    Task<CharacterClassJobOuterDtoV3> GetClassJobsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/Minions/{{lodestoneId}}")]
    Task<CollectionDtoV3<CharacterMinionDto>> GetMinionsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/Mounts/{{lodestoneId}}")]
    Task<CollectionDtoV3<CharacterMountDto>> GetMountsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/Achievements/{{lodestoneId}}")]
    Task<CharacterAchievementOuterDtoV3> GetAchievementsAsync(string lodestoneId, int? maxAge = null,
        FallbackType useFallback = FallbackType.None, CancellationToken cancellationToken = default);
}