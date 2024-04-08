using System.Linq.Expressions;

namespace YellowMark.AppServices.Specifications;

/// <summary>
/// Specification base class.
/// </summary>
/// <typeparam name="T">Type of Specification entity.</typeparam>
public abstract class Specification<T>
{
    /// <summary>
    /// Abstract ToExpression method for realisation in inherited classes.
    /// </summary>
    /// <returns>Expression</returns>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// Returns whether an object matches a specification expression.
    /// </summary>
    /// <param name="entity">Specification entity.</param>
    /// <returns>Bool</returns>
    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    /// <summary>
    /// Performs binary 'AND' operation between this Specification and other with the same entity type.
    /// </summary>
    /// <param name="specification"><see cref="Specification"/></param>
    /// <returns>Returns a new specification which is the result of the operation 'AND' between two other.</returns>
    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    /// <summary>
    /// Performs binary 'OR' operation between this Specification and other with the same entity type.
    /// </summary>
    /// <param name="specification"><see cref="Specification"/></param>
    /// <returns>Returns a new specification which is the result of the operation 'OR' between two other.</returns>
    public Specification<T> Or(Specification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    /// <summary>
    /// Performs unary 'NOT' operation on this Specification.
    /// </summary>
    /// <param name="specification"><see cref="Specification"/></param>
    /// <returns>Returns a new specification which is the result of the operation 'NOT' on this one.</returns>
    public Specification<T> Not(Specification<T> specification)
    {
        return new NotSpecification<T>(this);
    }
}
