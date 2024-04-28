namespace SimpleWebApi.Database.Extensions;

public static class Extensions
{
    public static void CallMethod(this Type type, string methodName, Type[] genericParameter, params object[] parameter)
    {
        type?.GetMethod(methodName)?.MakeGenericMethod(genericParameter)?.Invoke(null, parameter);
    }
}