using System.Reflection;
using Refit;

namespace NetStone.Api.Sdk.Refit;

/// <summary>
///     Refit does not support flags enums in query parameters by default. This smol formatter adds support for them.
/// </summary>
internal class RefitFlagEnumFormatter : DefaultUrlParameterFormatter
{
    public override string? Format(object? value, ICustomAttributeProvider attributeProvider, Type type)
    {
        if (value is not null && type.IsEnum &&
            type.CustomAttributes.Any(x => x.AttributeType == typeof(FlagsAttribute)))
        {
            return value.ToString()?.Replace(" ", "");
        }

        return base.Format(value, attributeProvider, type);
    }
}