using System.Linq.Expressions;

namespace YellowMark.AppServices.Specifications;

/// <summary>
/// 'OR' implementation of the Specification.
/// </summary>
/// <typeparam name="T">Type of Specification entity.</typeparam>
public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _leftSpecification;
    private readonly Specification<T> _rightSpecification;

    /// <summary>
    /// 'OR' specification constructor. 
    /// </summary>
    /// <param name="left">Left <see cref="Specification"/> operand in 'OR' expression.</param>
    /// <param name="right">Right <see cref="Specification"/> operand in 'OR' expression.</param>
    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _leftSpecification = left;
        _rightSpecification = right;
    }

    /// <summary>
    /// 'OR' implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _leftSpecification.ToExpression();
        Expression<Func<T, bool>> rightExpression = _rightSpecification.ToExpression();

        BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
    }
}
