using NetStone.Common.DTOs.Character;
using NetStone.Common.Enums;
using NetStone.Common.Queries;
using Refit;

namespace NetStone.Api.Sdk.Abstractions;

[Headers("Authorization: Bearer", "X-API-Version: 4")]
public interface INetStoneApiCharacter
{
    private const string Base = "/Character";

    [Post($"{Base}/Search")]
    Task<CharacterSearchPageDto> SearchAsync(CharacterSearchQuery query, short page = 1,
        CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}")]
    Task<CharacterDto> GetAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{world}}/{{name}}")]
    Task<CharacterDto> GetByNameAsync(string name, string world, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}/ClassJobs")]
    Task<CharacterClassJobOuterDto> GetClassJobsAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}/Minions")]
    Task<CollectionDto<CharacterMinionDto>> GetMinionsAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}/Mounts")]
    Task<CollectionDto<CharacterMountDto>> GetMountsAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}/Achievements")]
    Task<CharacterAchievementOuterDto> GetAchievementsAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);
}