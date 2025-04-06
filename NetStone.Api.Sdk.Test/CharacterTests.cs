using System.Net;
using NetStone.Api.Sdk.Abstractions;
using NetStone.Api.Sdk.Test.DataGenerators;
using NetStone.Common.Queries;
using Refit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace NetStone.Api.Sdk.Test;

public class CharacterTests(ITestOutputHelper testOutputHelper, CommonTestsFixture fixture)
    : TestBed<CommonTestsFixture>(testOutputHelper, fixture)
{
    private readonly INetStoneApiCharacter _character = fixture.GetService<INetStoneApiCharacter>(testOutputHelper)!;

    [Theory]
    [ClassData(typeof(CharacterSearchDataGenerator))]
    public async Task ClientIsReceivingCharacterSearch(CharacterSearchQuery query)
    {
        var result = await _character.SearchAsync(query);
        Assert.NotNull(result);
        Assert.True(result.HasResults);
    }

    [Theory]
    [ClassData(typeof(CharacterDataGenerator))]
    public async Task ClientIsReceivingCharacters(string lodestoneId)
    {
        var result = await _character.GetAsync(lodestoneId, 0);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ClientIsThrowingNotFoundException()
    {
        var validationApiException = await Assert.ThrowsAsync<ValidationApiException>(async () =>
            await _character.GetAsync("99999999", 0));
        Assert.Equal(HttpStatusCode.NotFound, validationApiException.StatusCode);
    }

    [Theory]
    [ClassData(typeof(CharacterDataGenerator))]
    public async Task ClientIsReceivingCharacterClassJobs(string lodestoneId)
    {
        var result = await _character.GetClassJobsAsync(lodestoneId, 0);
        Assert.NotNull(result);
    }

    [Theory]
    [ClassData(typeof(CharacterDataGenerator))]
    public async Task ClientIsReceivingCharacterMinions(string lodestoneId)
    {
        if (lodestoneId is "45386124") // Testerinus Maximus, Phoenix)
        {
            // test character has no minions
            var validationApiException = await Assert.ThrowsAsync<ValidationApiException>(async () =>
                await _character.GetMinionsAsync(lodestoneId, 0));

            Assert.Equal(HttpStatusCode.NotFound, validationApiException.StatusCode);

            return;
        }

        var result = await _character.GetMinionsAsync(lodestoneId, 0);
        Assert.NotNull(result);
    }

    [Theory]
    [ClassData(typeof(CharacterDataGenerator))]
    public async Task ClientIsReceivingCharacterMounts(string lodestoneId)
    {
        if (lodestoneId is "45386124" or "28835226") // Testerinus Maximus; Hena Wilbert; both Phoenix)
        {
            // test characters have no mounts
            var validationApiException = await Assert.ThrowsAsync<ValidationApiException>(async () =>
                await _character.GetMountsAsync(lodestoneId, 0));

            Assert.Equal(HttpStatusCode.NotFound, validationApiException.StatusCode);

            return;
        }

        var result = await _character.GetMountsAsync(lodestoneId, 0);
        Assert.NotNull(result);
    }

    [Theory]
    [ClassData(typeof(CharacterDataGenerator))]
    public async Task ClientIsReceivingCharacterAchievements(string lodestoneId)
    {
        var result = await _character.GetAchievementsAsync(lodestoneId, 0);
        Assert.NotNull(result);

        // achievements seem to be private by default, so most of these lists are empty. test the ones that are not.
        if (lodestoneId is "16303557" or "19060231" or "28812634" or "3386216" or "51569642")
        {
            Assert.NotEmpty(result.Achievements);
        }
    }
}