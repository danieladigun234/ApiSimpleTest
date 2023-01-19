using Newtonsoft.Json;
using System.Security.Claims;

var builder = GetApplicationBuilder();

WebApplicationBuilder GetApplicationBuilder()
{
    var webApplicationBuilder = WebApplication.CreateBuilder(args);
    LoadApplicationServices(webApplicationBuilder);

    return webApplicationBuilder;
}

WebApplication BuildWebApplication(WebApplicationBuilder webApplicationBuilder)
{
    var webApplication = webApplicationBuilder.Build();

    webApplication.UseAuthentication();
    webApplication.UseAuthorization();

    return webApplication;
}

WebApplication InitializeApplication(WebApplicationBuilder webApplicationBuilder)
{
    var app = BuildWebApplication(webApplicationBuilder);

    app.MapGet("/", () => "Hello World!");
    app.MapGet("/api/status", async context =>
    {
        context.Response.ContentType = "application/json";
        var message = new
        {
            Api = "Simple API Version 1.0",
            message = "API up and running..",
            port = app.Environment
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(message));
    });

    return app;
}

void LoadApplicationServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddControllers();
    webApplicationBuilder.Services.AddEndpointsApiExplorer();
}

void Run()
{
    InitializeApplication(builder).Run();
}

Run();
