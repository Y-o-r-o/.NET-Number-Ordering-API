using System.Net;

namespace BusinessLayer.DTOs;

public class ValidationHttpExceptionDTO : HttpExceptionDTO
{
    public ValidationHttpExceptionDTO(KeyValuePair<string, IEnumerable<string>> propertyErrors, AdditionalDetailsDTO? details = null)
    {
        PropertyErrors = propertyErrors;
        Details = details;
        StatusCode = HttpStatusCode.BadRequest;
    }

    /// <summary>
    /// Property error messages.
    /// </summary>
    public KeyValuePair<string, IEnumerable<string>> PropertyErrors { get; set; }
}