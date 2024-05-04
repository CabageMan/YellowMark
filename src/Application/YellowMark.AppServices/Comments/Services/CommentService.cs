using AutoMapper;
using YellowMark.AppServices.Accounts.Services;
using YellowMark.AppServices.Ads.Services;
using YellowMark.AppServices.Comments.Exceptions;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.AppServices.Comments.Specifications;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;
using YellowMark.Domain.Comments.Entity;
using YellowMark.Domain.UserRoles;

namespace YellowMark.AppServices.Comments.Services;

/// <inheritdoc cref="ICommentService"/>
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAccountService _accountService;
    private readonly IAdService _adService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="CommentService"/> instance.
    /// </summary>
    /// <param name="commentRepository">Comment repository <see cref="ICommentRepository"/></param>
    /// <param name="accountService">Account service <see cref="IAccountService"/></param>
    /// <param name="adService">Account service <see cref="IAdService"/></param>
    /// <param name="mapper">Ads mapper.</param>
    public CommentService(
        ICommentRepository commentRepository,
        IAccountService accountService,
        IAdService adService,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _accountService = accountService;
        _adService = adService;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddCommentAsync(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var currentUserInfo = await _accountService.GetAccountInfoAssync(cancellationToken);
        CommentOperationException.ThrowIfNull(currentUserInfo, "Could not get user info for comment creation.");

        var commentedAdExists = await _adService.AdExistsWithId(request.AdId, cancellationToken);
        CommentOperationException.ThrowIfFalse(commentedAdExists, $"There is no ad with id: {request.AdId}");

        var entity = _mapper.Map<CreateCommentRequest, Comment>(request);
        entity.UserId = currentUserInfo.Id;

        await _commentRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<CommentDto> GetCommentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _commentRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<CommentDto>(entity);
    }

    /// <inheritdoc/>
    public Task<ResultWithPagination<CommentDto>> GetCommentsAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        return _commentRepository.GetAllAsync(request, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<CommentDto>> GetCommentsByStringAsync(string searchRequestString, CancellationToken cancellationToken)
    {
        Specification<Comment> specification = new CommentByTextOrAuthorSpecification(searchRequestString);
        return _commentRepository.GetFiltered(specification, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CommentDto> UpdateCommentAsync(Guid id, UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        var currentUserInfo = await _accountService.GetAccountInfoAssync(cancellationToken);
        CommentOperationException.ThrowIfNull(currentUserInfo, "Could not get user info for comment update.");

        var currentEntity = await _commentRepository.GetByIdAsync(id, cancellationToken);
        CommentNotFoundException.ThrowIfNull(currentEntity, "Could not get comment for update.");

        if (currentUserInfo.Id != currentEntity.UserId)
        {
            throw new CommentPermissionsDeniedException("Current user can not update this comment.");
        }

        var updatedEntity = _mapper.Map<UpdateCommentRequest, Comment>(request);
        updatedEntity.Id = id;
        updatedEntity.CreatedAt = currentEntity.CreatedAt;
        updatedEntity.AdId = currentEntity.AdId;

        await _commentRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<Comment, CommentDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteCommentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var currentUserInfo = await _accountService.GetAccountInfoAssync(cancellationToken);
        CommentOperationException.ThrowIfNull(currentUserInfo, "Could not get user info for comment deletion.");

        var currentEntity = await _commentRepository.GetByIdAsync(id, cancellationToken);
        CommentNotFoundException.ThrowIfNull(currentEntity, "There is nothing to delete.");

        if (
            currentUserInfo.Id != currentEntity.UserId ||
            currentUserInfo.UserRoles.Contains(UserRoles.Admin) ||
            currentUserInfo.UserRoles.Contains(UserRoles.SuperUser))
        {
            throw new CommentPermissionsDeniedException("Current user can not delete this comment.");
        }

        await _commentRepository.DeleteAsync(id, cancellationToken);
    }
}
