namespace BusinessLayer.DTOs;

public class AdditionalDetailsDTO
{
    /// <summary>
    /// Inner exception message.
    /// </summary>
    /// <example>Optional details of error.</example>
    public string? InnerMessage { get; set; }

    /// <summary>
    /// File in which exception was triggered.
    /// </summary>
    /// <example>C:\\Project\\API\\Controllers\\SomeController.cs</example>
    public string? File { get; set; }

    /// <summary>
    /// Method name
    /// </summary>
    /// <example>GetSomething</example>
    public string? MethodName { get; set; }

    /// <summary>
    /// Line in which exception was triggered.
    /// </summary>
    /// <example>26</example>
    public int Line { get; set; }

    /// <summary>
    /// Column in which exception was triggered.
    /// </summary>
    /// <example>9</example>
    public int Column { get; set; }
}

