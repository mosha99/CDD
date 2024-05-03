using SimpleWebApi.Requests.AirPlane;

namespace SimpleWebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FromPathAttribute(int index) : SwaggerBodyIgnoreAttribute
{
    public int Index { get; set; } = index;
}