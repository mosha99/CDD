using FluentValidation;
using SimpleWebApi.Infrastructure.Exceptions.Base;
using SimpleWebApi.Infrastructure.Validations;
using FluentValidation.Results;

namespace SimpleWebApi.Validator;

public class EntityValidation
{
    public class FluentValidation<T>(IServiceProvider serviceProvider) : IEntityValidation<T>
    {
        public async Task ValidateAsync(T entity)
        {

            IValidator<T> validator = serviceProvider.GetService<IValidator<T>>()!;

            if (validator is null) return;

            var validationResult = await validator.ValidateAsync(entity);

            if (validationResult.IsValid) return;

            throw new EntityValidationException(validationResult);
        }

        public class EntityValidationException(ValidationResult validationResult) : Exception(), IException
        {
            public int GetStatsCode() => 422;

            public string GetTitle() => "Model Not Valid";

            public object GetResult() => validationResult;
        }
    }

}