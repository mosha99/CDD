using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.EntityBase;
using SimpleWebApi.Infrastructure.Filter;
using SimpleWebApi.Infrastructure.Request.Querys;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IGetAllCommand<TEntity, TFilter, TDto> : IRequest<ListResult<TDto>>, IBaseListQuery<TEntity, TFilter>
    where TEntity : IEntity
    where TFilter : BaseFilter<TEntity>;