using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Comments.Services;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;
using YellowMark.Domain.UserRoles;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Comment controller.
/// </summary>
[ApiController]
[Route("api/v1/comments")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IValidator<CreateCommentRequest> _commentValidator;
    private readonly IValidator<UpdateCommentRequest> _updateCommentValidator;
    private readonly IValidator<GetAllRequestWithPagination> _paginationRequestValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="CommentController"/>
    /// </summary>
    /// <param name="commentService">Comments service.</param>
    /// <param name="commentValidator">Comment creation validator.</param>
    /// <param name="updateCommentValidator">Comment update validator.</param>
    /// <param name="paginationRequestValidator">Pagination params validator</param>
    /// <param name="guidValidator"></param>
    public CommentController(
        ICommentService commentService,
        IValidator<CreateCommentRequest> commentValidator,
        IValidator<UpdateCommentRequest> updateCommentValidator,
        IValidator<GetAllRequestWithPagination> paginationRequestValidator,
        IValidator<Guid> guidValidator)
    {
        _commentService = commentService;
        _commentValidator = commentValidator;
        _updateCommentValidator = updateCommentValidator;
        _paginationRequestValidator = paginationRequestValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new Comment.
    /// </summary>
    /// <param name="request">Comment request model <see cref="CreateCommentRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created comment Id <see cref="Guid"/></returns>
    [Authorize]
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
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [HttpGet]
    [ProducesResponseType(typeof(ResultWithPagination<CommentDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllComments([FromQuery] GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var validationResult = await _paginationRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _commentService.GetCommentsAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return comment by id.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Comment.</returns>
    [Authorize]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCommentById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _commentService.GetCommentByIdAsync(id, cancellationToken);
        if (result == null)
        {
            return NotFound("Could not find comment.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Returns comments list filtered by text, author name or last name.
    /// </summary>
    /// <param name="searchString">Search string to filter comments</param> 
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Comments list.</returns>
    [Authorize]
    [HttpGet]
    [Route("by-text-or-author")]
    [ProducesResponseType(typeof(IEnumerable<CommentDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllByTextOrAuthor([FromQuery] string searchString, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetCommentsByStringAsync(searchString, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return ad comments.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>List of the <see cref="CommentDto"/>.</returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("~/api/v1/ads/{id:Guid}/comments")]
    [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetCommentsByAdId(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _commentService.GetCommentsByAdIdAsync(id, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Update Comment by Id.
    /// Any User can update only own comment. 
    /// </summary>
    /// <param name="id">Comment Id to update.</param>
    /// <param name="request">Update comment request model <see cref="UpdateCommentRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated comment <see cref="CommentDto"/></returns>
    [Authorize]
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateComment(Guid id, UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!guidValidationResult.IsValid)
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var commentValidationResult = await _updateCommentValidator.ValidateAsync(request, cancellationToken);
        if (!commentValidationResult.IsValid)
        {
            return BadRequest(commentValidationResult.ToString());
        }

        var updatedComment = await _commentService.UpdateCommentAsync(id, request, cancellationToken);
        if (updatedComment == null)
        {
            return NotFound("Could not find comment to update.");
        }

        return Ok(updatedComment);
    }

    /// <summary>
    /// Delete user comment by Id. 
    /// User can delete only own comment. 
    /// Admin or Superuser can delete any comment.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [Authorize]
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

        return NoContent();
    }
}
