using System.Net;

namespace Core.Exceptions;

public class HttpResponseException(HttpStatusCode statusCode, string? message = null) : System.Web.Http.HttpResponseException(statusCode)
{
    private readonly string? _message = message;

    private readonly string _statusCodeMessage = statusCode.ToString();

    public HttpStatusCode StatusCode { get; } = statusCode;

    public override string Message => _message is null ? _statusCodeMessage : _message;

}