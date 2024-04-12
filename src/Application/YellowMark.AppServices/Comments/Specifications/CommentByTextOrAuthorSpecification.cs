using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.AppServices.Comments.Specifications;

/// <summary>
/// Get Comment by text, author name or lastname implementation of the Specification.
/// </summary>
public class CommentByTextOrAuthorSpecification : Specification<Comment>
{
    private readonly string _searchString;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="searchString">Search string to find any comments.</param>
    public CommentByTextOrAuthorSpecification(string searchString)
    {
        _searchString = searchString;
    }

    /// <summary>
    /// Get Comment by text, author name or lastname implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return comment => comment.Text.Contains(_searchString) 
            || comment.User.FirstName.Contains(_searchString)
            || comment.User.LastName.Contains(_searchString);
    }
}
