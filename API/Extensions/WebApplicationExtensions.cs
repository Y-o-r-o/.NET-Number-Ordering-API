using API.Middleware;

namespace API.Extensions;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app, IConfiguration config)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    config.GetValue<string>("NumberOrderingSwaggerEndpoint:apiUri"),
                    config.GetValue<string>("NumberOrderingSwaggerEndpoint:apiTitle")
                    );
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}