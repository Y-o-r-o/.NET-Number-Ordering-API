using Core.Enums;

namespace Core;

internal static class Constants
{
    public static readonly Dictionary<StringIs, PropertyValidationConfiguration> PROPERTY_VALIDATIONS = new()
    {
        {StringIs.FileName, new(@"^[^\x00-\x1F\xA5\\?*:\"";|\/<>]+$", "can't contain any of the following characters `\\`, `/`, `:`, `*`, `?`, `\"`, `<`, `>`, `|`.", 256, 1)},
        {StringIs.Path, new(@"^(?:[a-zA-Z]:|(?:\\\\|\\/)[\w.$●-]+(?:\\\\|\\/)[\w.$●-]+)\\(?:[^\\/:*?\""<>|\r\n]+\\)*[^\\/:*?\""<>|\r\n]*$", "is not a valid directory path.", 256, 3)},
        {StringIs.NumbersLine, new(@"^-?(?:[1-9]\d*|0)(?: -?(?:[1-9]\d*|0))*$", "should only consist of numbers separated by white space.", int.MaxValue, 1)}
    };
}