namespace SimpleWebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ConvertAbleToAttribute(Type target) : Attribute
{
    public Type Target { get; } = target;
}