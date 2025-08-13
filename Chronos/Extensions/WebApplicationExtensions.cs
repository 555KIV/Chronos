namespace Chronos.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.MapOpenApi();
        
        return app;
    }
}