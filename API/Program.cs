using API.Extensions;

namespace API;

internal sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices(builder.Configuration);

        var app = builder.Build();
        using var scope = app.Services.CreateScope();

        app.Configure(builder.Configuration);

        app.Run();
    }
}