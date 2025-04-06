using NetStone.Api.Sdk.Abstractions;
using NetStone.Api.Sdk.Test.DataGenerators;
using NetStone.Common.Queries;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace NetStone.Api.Sdk.Test;

public class FreeCompanyTests(ITestOutputHelper testOutputHelper, CommonTestsFixture fixture)
    : TestBed<CommonTestsFixture>(testOutputHelper, fixture)
{
    private readonly INetStoneApiFreeCompany _freeCompany =
        fixture.GetService<INetStoneApiFreeCompany>(testOutputHelper)!;

    [Theory]
    [ClassData(typeof(FreeCompanySearchDataGenerator))]
    public async Task ClientIsReceivingFreeCompanySearch(FreeCompanySearchQuery query)
    {
        var result = await _freeCompany.SearchAsync(query);
        Assert.NotNull(result);
        Assert.True(result.HasResults);
    }

    [Theory]
    [ClassData(typeof(FreeCompanyTestsDataGenerator))]
    public async Task ClientIsReceivingFreeCompanies(string lodestoneId)
    {
        var result = await _freeCompany.GetAsync(lodestoneId, 0);
        Assert.NotNull(result);
    }

    [Theory]
    [ClassData(typeof(FreeCompanyTestsDataGenerator))]
    public async Task ClientIsReceivingFreeCompanyMembers(string lodestoneId)
    {
        var result = await _freeCompany.GetMembersAsync(lodestoneId, 0);
        Assert.NotNull(result);
        Assert.NotEmpty(result.Members);
    }
}