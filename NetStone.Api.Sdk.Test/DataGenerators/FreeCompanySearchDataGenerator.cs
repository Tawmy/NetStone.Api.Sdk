using NetStone.Common.Queries;

namespace NetStone.Api.Sdk.Test.DataGenerators;

public class FreeCompanySearchDataGenerator : TheoryData<FreeCompanySearchQuery>
{
    public FreeCompanySearchDataGenerator()
    {
        Add(new FreeCompanySearchQuery("Dust Bunnies", "Phoenix"));
    }
}