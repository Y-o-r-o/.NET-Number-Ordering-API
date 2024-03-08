using API.Extensions;
using BusinessLayer.DTOs;
using Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;
    private readonly JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpResponseException ex)
        {
            _logger.LogError(ex, ex.Response.ToString());
            await HandleExceptionAsync(context, ex, ex.Response.StatusCode);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, ex.Response.ToString());
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        var response = new HttpExceptionDTO()
        {
            StatusCode = httpStatusCode,
            Message = exception.Message,
            Details = _env.IsDevelopment() ? exception.AnalyzeException() : null
        };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;

        var response = new ValidationHttpExceptionDTO()
        {
            PropertyErrors = exception.VariableErrors,
            Details = _env.IsDevelopment() ? exception.AnalyzeException() : null
        };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
