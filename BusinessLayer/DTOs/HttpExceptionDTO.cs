using System.Net;

namespace BusinessLayer.DTOs;

public class HttpExceptionDTO
{
    /// <summary>
    /// Http error message
    /// </summary>
    /// <example>Internal server error.</example>
    public string Message { get; set; }

    /// <summary>
    /// Http status code
    /// </summary>
    /// <example>500</example>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Http error details (dev only)
    /// </summary>
    public AdditionalDetailsDTO? Details { get; set; }
}

