using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.AppServices.Comments.Specifications;

/// <summary>
/// Get Comment by text, author name or lastname implementation of the Specification.
/// </summary>
public class CommentByTextOrAuthorSpecification : Specification<Comment>
{
    private readonly string _searchStringLoweCase;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="searchString">Search string to find any comments.</param>
    public CommentByTextOrAuthorSpecification(string searchString)
    {
        _searchStringLoweCase = searchString.ToLower();
    }

    /// <summary>
    /// Get Comment by text, author name or lastname implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return comment => comment.Text.ToLower().Contains(_searchStringLoweCase) 
            || comment.UserInfo.FirstName.ToLower().Contains(_searchStringLoweCase)
            || comment.UserInfo.LastName.ToLower().Contains(_searchStringLoweCase);
    }
}
