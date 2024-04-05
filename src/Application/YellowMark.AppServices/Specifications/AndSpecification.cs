using System.Linq.Expressions;

namespace YellowMark.AppServices.Specifications;

/// <summary>
/// 'AND' implementation of the Specification.
/// </summary>
/// <typeparam name="T">Type of Specification entity.</typeparam>
public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _leftSpecification;
    private readonly Specification<T> _rightSpecification;

    /// <summary>
    /// 'AND' specification constructor. 
    /// </summary>
    /// <param name="left">Left <see cref="Specification"/> operand in 'AND' expression.</param>
    /// <param name="right">Right <see cref="Specification"/> operand in 'AND' expression.</param>
    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _leftSpecification = left;
        _rightSpecification = right;
    }
    
    /// <summary>
    /// 'AND' implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _leftSpecification.ToExpression();
        Expression<Func<T, bool>> rightExpression = _rightSpecification.ToExpression();

        BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
    }
}
