using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Requests.AirPlane;

namespace SimpleWebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HttpMethodAttribute(HttpMethodEnum method) : Attribute
{
    public HttpMethodEnum Method { get; } = method;
}