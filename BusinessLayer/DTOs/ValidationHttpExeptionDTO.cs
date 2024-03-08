using System.Net;

namespace BusinessLayer.DTOs;

public class ValidationHttpExceptionDTO : HttpExceptionDTO
{
    public ValidationHttpExceptionDTO()
    {
        StatusCode = HttpStatusCode.BadRequest;
    }

    /// <summary>
    /// Property error messages.
    /// </summary>
    public KeyValuePair<string, IEnumerable<string>> PropertyErrors { get; set; }
}