using System.Net;
using System.Text.RegularExpressions;
using Core.Enums;
using Core.Exceptions;

namespace Core.Extensions;

public static class StringExtensions
{
    public static void Validate(this string str, StringIs stringIs, string stringName = "string")
    {
        var errors = new List<string>();

        if (!Constants.PropertyValidations.TryGetValue(stringIs, out PropertyValidationConfiguration? validationParameters))
        {
            throw new HttpResponseException(HttpStatusCode.InternalServerError, $"There is no property validation parameters for enum: {stringIs}.");
        };

        if (str.Length < validationParameters.MinimumNameLength || str.Length > validationParameters.MaximumNameLength)
        {
            errors.Add($"`{stringName}` {validationParameters.LengthErrorMessage}");
        }

        if (!Regex.IsMatch(str, validationParameters.ValidationRegex))
        {
            errors.Add($"`{stringName}` {validationParameters.RegexErrorMessage}");
        }

        ValidationException.ThrowIfPropertyInvalid(stringName, errors);
    }

    public static List<int> ConvertToIntList(this string whiteSpaceSeparatedIntString)
    {
        return whiteSpaceSeparatedIntString.Equals("") ? [] : whiteSpaceSeparatedIntString.Split(' ').Select(Int32.Parse).ToList() ?? [];
    }
}