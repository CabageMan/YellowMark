using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Comments.Services;
using YellowMark.Contracts;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Comment controller.
/// </summary>
[ApiController]
[Route("v1/comments")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IValidator<CreateCommentRequest> _commentValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="CommentController"/>
    /// </summary>
    /// <param name="commentService"></param>
    /// <param name="commentValidator"></param>
    /// <param name="guidValidator"></param>
    public CommentController(
        ICommentService commentService,
        IValidator<CreateCommentRequest> commentValidator,
        IValidator<Guid> guidValidator
    )
    {
        _commentService = commentService;
        _commentValidator = commentValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new Comment.
    /// </summary>
    /// <param name="request">Comment request model <see cref="CreateCommentRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created comment Id <see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateComment(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _commentValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var addedCommentId = await _commentService.AddCommentAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedCommentId}", UriKind.Relative), addedCommentId);
    }

    /// <summary>
    /// Returns all comments list with pagination.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/>.</param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Comments list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResultWithPagination<CommentDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllComments([FromQuery] GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        // TODO: Implement pagination validator.
        // TODO: Implement all possible returning status codes.
        var result = await _commentService.GetCommentsAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return comment by id.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Comment.</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetCommentById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _commentService.GetCommentByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Returns comments list filtered by text, author name or last name.
    /// </summary>
    /// <param name="searchString">Search string to filter comments</param> 
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Comments list.</returns>
    [HttpGet]
    [Route("by-text-or-author")]
    [ProducesResponseType(typeof(IEnumerable<CommentDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllByTextOrAuthor([FromQuery] string searchString, CancellationToken cancellationToken)
    {
        // TODO: Implement all possible returning status codes.
        var result = await _commentService.GetCommentsByStringAsync(searchString, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update Comment by Id.
    /// </summary>
    /// <param name="id">Needed to update comment.</param>
    /// <param name="request">Comment request model <see cref="CreateCommentRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated comment <see cref="CommentDto"/></returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateComment(Guid id, CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!guidValidationResult.IsValid)
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var commentValidationResult = await _commentValidator.ValidateAsync(request, cancellationToken);
        if (!commentValidationResult.IsValid)
        {
            return BadRequest(commentValidationResult.ToString());
        }
        // TODO: Handle exceptions

        var updatedAd = await _commentService.UpdateCommentAsync(id, request, cancellationToken);
        return Ok(updatedAd);
    }

    /// <summary>
    /// Delete Comment by Id.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteComment(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        await _commentService.DeleteCommentByIdAsync(id, cancellationToken);
        // TODO: Handle exceptions

        return NoContent();
    }
}
