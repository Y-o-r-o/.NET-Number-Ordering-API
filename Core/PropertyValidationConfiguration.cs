internal class PropertyValidationConfiguration(string validationRegex, string regexErrorMessage, int maximumNameLength, int minimumNameLength)
{
    public readonly string ValidationRegex = validationRegex;
    public readonly string RegexErrorMessage = regexErrorMessage;
    public readonly int MinimumNameLength = minimumNameLength;
    public readonly int MaximumNameLength = maximumNameLength;
    public string LengthErrorMessage => $"length should be between {MinimumNameLength} and {MaximumNameLength}.";
}