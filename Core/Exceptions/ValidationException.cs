using System.Net;

namespace Core.Exceptions;

public class ValidationException : System.Web.Http.HttpResponseException
{
    public KeyValuePair<string, IEnumerable<string>> VariableErrors { get; set; }

    public HttpStatusCode StatusCode { get; }

    public ValidationException(string propertyName, IEnumerable<string> errors) : base(HttpStatusCode.BadRequest)
    {
        StatusCode = HttpStatusCode.BadRequest;

        VariableErrors = new(propertyName, errors);
    }

    public ValidationException(string propertyName, string error) : base(HttpStatusCode.BadRequest)
    {
        StatusCode = HttpStatusCode.BadRequest;

        VariableErrors = new(propertyName, [error]);
    }

    public static void ThrowIfPropertyInvalid(string propertyName, IReadOnlyCollection<string> errors)
    {
        if (errors != null && errors.Count > 0)
        {
            throw new ValidationException(propertyName, errors);
        }
    }
}