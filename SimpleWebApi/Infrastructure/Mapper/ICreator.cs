namespace SimpleWebApi.Infrastructure.Mapper;

public interface ICreator<out TEntity>
{
    public TEntity Create();
}