using NetStone.Common.DTOs.FreeCompany;
using NetStone.Common.Enums;
using NetStone.Common.Queries;
using Refit;

namespace NetStone.Api.Sdk.Abstractions;

[Headers("Authorization: Bearer", "X-API-Version: 4")]
public interface INetStoneApiFreeCompany
{
    private const string Base = "/FreeCompany";

    [Post($"{Base}/Search")]
    Task<FreeCompanySearchPageDto> SearchAsync(FreeCompanySearchQuery query, short page = 1,
        CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}")]
    Task<FreeCompanyDto> GetAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{world}}/{{name}}")]
    Task<FreeCompanyDto> GetByNameAsync(string name, string world, CancellationToken cancellationToken = default);

    [Get($"{Base}/{{lodestoneId}}/Members")]
    Task<FreeCompanyMembersOuterDto> GetMembersAsync(string lodestoneId, int? maxAge = null,
        FallbackTypeV4 useFallback = FallbackTypeV4.None, CancellationToken cancellationToken = default);
}