using System.Reflection;
using MediatR;
using SimpleWebApi.Database.Extensions;
using SimpleWebApi.Infrastructure.MediatR;
 

namespace SimpleWebApi.Extensions;

public static class AggregateHandlerExtension
{
    public static void RegisterAggregateHandlers(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        List<Type> allTypes = assemblies.SelectMany(x => x.GetTypes()).ToList();

        var types = allTypes.Where( x=> typeof(IDefaultCommand).IsAssignableFrom(x) && x is { IsClass: true, IsAbstract: false })
            .SelectMany(x => x.GetInterfaces().Where(typeof(IDefaultCommand).IsAssignableFrom).Where(type => type.IsGenericType)).ToList();

        foreach (var type in types)
        {
            var genericTypeArguments = type.GenericTypeArguments;
            var genericDefinitionTypes = type.GetGenericTypeDefinition().GetGenericArguments();

            Dictionary<string, Type> genericArgsDictionary = [];

            for (int i = 0; i < genericTypeArguments.Length; i++)
                genericArgsDictionary.Add(genericDefinitionTypes[i].Name, genericTypeArguments[i]);

            foreach (var nestedType in type.GetNestedTypes())
            {
                var handlerType = nestedType.GetInterface(typeof(IRequestHandler<,>).Name);
                var generatedType = GetGenericType(handlerType!, genericArgsDictionary);

                var request = generatedType.GenericTypeArguments[0];
                var commands = allTypes?.Where(x => x == request || (x.IsClass && !x.IsAbstract & request.IsAssignableFrom(x))).ToArray();

                var response = generatedType.GenericTypeArguments[1];
                var result = allTypes?.Where(x => x == response || (x.IsClass && !x.IsAbstract & response.IsAssignableFrom(x))).SingleOrDefault() ?? response;

                foreach (var type1 in commands!)
                {
                    typeof(AggregateHandlerExtension).CallMethod("Register", [type1, result, nestedType.MakeGenericType(genericTypeArguments)], serviceCollection);
                }
            }

        }
    }

    public static Type GetGenericType(Type type, Dictionary<string, Type> genericArgsDictionary)
    {
        if (!type.ContainsGenericParameters) return type;
        if (genericArgsDictionary.TryGetValue(type.Name, out var castedType)) return castedType;
        var genericArg = type.GenericTypeArguments.Select(x => GetGenericType(x, genericArgsDictionary)).ToArray();
        return type.GetGenericTypeDefinition().MakeGenericType(genericArg);
    }

    public static void Register<TRequest, TResponse, THandler>(IServiceCollection serviceCollection)
        where TRequest : IRequest<TResponse>
        where THandler : class, IRequestHandler<TRequest, TResponse>
    {
        serviceCollection.AddScoped<IRequestHandler<TRequest, TResponse>, THandler>();
    }
}