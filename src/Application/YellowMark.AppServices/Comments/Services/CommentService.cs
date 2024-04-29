using AutoMapper;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.AppServices.Comments.Specifications;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.AppServices.Comments.Services;

/// <inheritdoc cref="ICommentService"/>
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="CommentService"/> instance.
    /// </summary>
    /// <param name="commentRepository">Comment repository <see cref="ICommentRepository"/></param>
    /// <param name="mapper">Ads mapper.</param>
    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddCommentAsync(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateCommentRequest, Comment>(request);
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
    public async Task<CommentDto> UpdateCommentAsync(Guid id, CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var currentEntity = await _commentRepository.GetByIdAsync(id, cancellationToken);

        var updatedEntity = _mapper.Map<CreateCommentRequest, Comment>(request);
        updatedEntity.Id = id;
        updatedEntity.CreatedAt = currentEntity.CreatedAt;

        await _commentRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<Comment, CommentDto>(updatedEntity);
    }
    
    /// <inheritdoc/>
    public async Task DeleteCommentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _commentRepository.DeleteAsync(id, cancellationToken);
    }
}
