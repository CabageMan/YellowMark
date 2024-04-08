using System.Linq.Expressions;

namespace YellowMark.AppServices.Specifications;

/// <summary>
/// 'NOT' implementation of the Specification.
/// </summary>
/// <typeparam name="T">Type of Specification entity.</typeparam>
public class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _specification;

    /// <summary>
    /// 'NOT' specification constructor. 
    /// </summary>
    /// <param name="specification">Specification in expression.</param>
    public NotSpecification(Specification<T> specification)
    {
        _specification = specification;
    }

    /// <summary>
    /// 'NOT' implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> expression = _specification.ToExpression();

        UnaryExpression notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
    }
}
