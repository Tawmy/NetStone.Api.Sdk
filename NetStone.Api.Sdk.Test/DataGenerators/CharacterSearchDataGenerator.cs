using NetStone.Common.Enums;
using NetStone.Common.Queries;

namespace NetStone.Api.Sdk.Test.DataGenerators;

internal class CharacterSearchDataGenerator : TheoryData<CharacterSearchQuery>
{
    public CharacterSearchDataGenerator()
    {
        Add(new CharacterSearchQuery("Alyx Bergen", "Phoenix", Language: Language.English | Language.German));
    }
}