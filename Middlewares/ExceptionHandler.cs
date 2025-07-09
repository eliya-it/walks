using System.Net;

namespace NZWalks.API.Middlewares;

public class ExceptionHandler
{
    private readonly ILogger<ExceptionHandler> logger;
    private readonly RequestDelegate requestDelegate;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate requestDelegate)
    {
        this.logger = logger;
        this.requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await requestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid();
            logger.LogError(ex, $"{errorId} : {ex.Message}");
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var error = new
            {
                Id = errorId,
                ErrorMessage = "Something went very wrong!",

            };
            await httpContext.Response.WriteAsJsonAsync(error);
        }

    }
}