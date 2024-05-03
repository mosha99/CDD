using SimpleWebApi.Executor;
using SimpleWebApi.Infrastructure.Exceptions;
using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi;

public static class RoutMapTools
{
    public static async Task SetExceptionResult(this HttpResponse response, Exception e)
    {
        IException exception = e as IException ?? new UnexpectedException();
        response.StatusCode = exception.GetStatsCode();
        var result = exception.GetResult();
        await response.WriteAsJsonAsync(result);
    }
    public static bool IsValidGetRequest(HttpContext context)
    {
        if (context.Request.Path.ToString().Split('/').Count(x => !string.IsNullOrWhiteSpace(x)) < 2) return false;
        return true;
    }
    public static async Task RequestHandler(HttpContext context)
    {
        try
        {
            var executor = context.RequestServices.GetService<CommandExecutor>();
            var response = await executor!.Execute(context.Request);
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception e)
        {
            await context.Response.SetExceptionResult(e);
        }
    }
}