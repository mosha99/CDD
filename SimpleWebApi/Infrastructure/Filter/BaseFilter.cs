using System.Linq.Expressions;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SimpleWebApi.Infrastructure.Filter;

public abstract class BaseFilter<TEntity>
{
    private readonly List<Expression<Func<TEntity, bool>>> _expressionList = [];

    public abstract void InitExpression();

    public void AddExpression(Expression<Func<TEntity, bool>> expression) => _expressionList.Add(expression);

    public void AddExpression<T>(T value, Expression<Func<TEntity, bool>> expression) where T : struct
    {
        if (!value.Equals(default)) AddExpression(expression);
    }  
    public void AddExpression<T>(T? value, Expression<Func<TEntity, bool>> expression) where T : struct
    {
        if (value != null) AddExpression(expression);
    }  

    public void AddExpression(string? value, Expression<Func<TEntity, bool>> expression)
    {
        if (! string.IsNullOrWhiteSpace(value)) AddExpression(expression);
    } 

    public void AddExpression(object? value, Expression<Func<TEntity, bool>> expression)
    {
        if (value is not null) AddExpression(expression);
    }
    public void AddExpression(bool expressionEffectCondition, Expression<Func<TEntity, bool>> expression)
    {
        if (expressionEffectCondition) AddExpression(expression);
    }

    public List<Expression<Func<TEntity, bool>>> GetAllExpression()
    {
        _expressionList.Clear();

        InitExpression();

        return _expressionList;
    }
}