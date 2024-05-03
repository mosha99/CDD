using MediatR;
using SimpleWebApi.Infrastructure.Validations;

namespace SimpleWebApi.Infrastructure.MediatR;

public sealed class ValidationBehavior<TRequest, TResponse>(IEntityValidation<TRequest> validator) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await validator.ValidateAsync(request);
        return await next();
    }
}