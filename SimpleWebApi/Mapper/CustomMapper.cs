using AutoMapper;
using AutoMapper.QueryableExtensions;
using SimpleWebApi.Infrastructure.Mapper;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleWebApi.Mapper;

public class CustomMapper
{
    public class AutoMapper(IMapper mapper) : ICustomMapper
    {
        public IQueryable<TRight> MapTo<TLeft, TRight>(IQueryable<TLeft> queryable)
        {
            return queryable.ProjectTo<TRight>(mapper.ConfigurationProvider);
        }

        public IEnumerable<TRight> MapTo<TLeft, TRight>(IEnumerable<TLeft> collection)
        {
            return MapTo<IEnumerable<TLeft>, IEnumerable<TRight>>(collection);
        }

        public TRight MapTo<TLeft, TRight>(TLeft entity)
        {
            if (entity is ICreator<TRight> creator) return creator.Create();
            return mapper.Map<TLeft, TRight>(entity);
        }

        public TResult MapTo<TResult>(object entity)
        {
            return (TResult)mapper.Map(entity, entity.GetType(), typeof(TResult));
        }
    }
}