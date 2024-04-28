using System.Reflection;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;
using SimpleWebApi.Infrastructure.Extencions;
using SimpleWebApi.Infrastructure.Filter;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Infrastructure.Request.Querys;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Implementation;

public class DefaultGetSpecification<TAggregate,TDto, TFilter>(BaseListQuery<TAggregate, TFilter> listQuery , ICustomMapper mapper) : IBaseGetListWithMappingSpecification<TAggregate,TDto>
    where TFilter : BaseFilter<TAggregate>
{
    public Task<ListResult<TDto>> GetAllAsync(IQueryable<TAggregate> queryable, CancellationToken cancellationToken)
    {
        return queryable.ToListResultAsync<TAggregate,TDto,TFilter>(listQuery, mapper, cancellationToken);
    }
}