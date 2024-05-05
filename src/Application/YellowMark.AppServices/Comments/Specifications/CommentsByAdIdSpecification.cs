using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.AppServices.Comments.Specifications;

/// <summary>
/// Get Comments list by Ad id implementation of the Specification.
/// </summary>
public class CommentsByAdIdSpecification : Specification<Comment>
{
    private readonly Guid _adId;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="adId">Ad id.</param>
    public CommentsByAdIdSpecification(Guid adId)
    {
        _adId = adId;
    }

    /// <summary>
    /// Get Comment by Ad id implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return comment => comment.AdId == _adId;
    }
}

