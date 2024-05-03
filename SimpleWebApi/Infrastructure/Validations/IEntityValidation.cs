
namespace SimpleWebApi.Infrastructure.Validations;

public interface IEntityValidation<in T>
{
    Task ValidateAsync(T entity);
}