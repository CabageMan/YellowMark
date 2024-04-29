using AutoMapper;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.AppServices.Ads.Specifications;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Pagination;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.AppServices.Ads.Services;

/// <inheritdoc cref="IAdService"/>
public class AdService : IAdService
{
    private readonly IAdRepository _adRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="AdService"/> instance.
    /// </summary>
    /// <param name="adRepository">Ad repository <see cref="IAdRepository"/></param>
    /// <param name="mapper">Ads mapper.</param>
    public AdService(IAdRepository adRepository, IMapper mapper)
    {
        _adRepository = adRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAdAsync(CreateAdRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateAdRequest, Ad>(request);
        await _adRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public Task<ResultWithPagination<AdDto>> GetAdsAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        return _adRepository.GetAllAsync(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<AdDto> GetAdByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _adRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<Ad, AdDto>(entity);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<AdDto>> GetAdsByTitleAsync(AdByTitleRequest request, CancellationToken cancellationToken)
    {
        Specification<Ad> specification = new AdByTitleSpecification(request.Title);
        return _adRepository.GetFiltered(specification, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<AdDto> UpdateAdAsync(Guid id, CreateAdRequest request, CancellationToken cancellationToken)
    {
        var currentEntity = await _adRepository.GetByIdAsync(id, cancellationToken);

        var updatedEntity = _mapper.Map<CreateAdRequest, Ad>(request);
        updatedEntity.Id = id;
        updatedEntity.CreatedAt = currentEntity.CreatedAt;

        await _adRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<Ad, AdDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteAdByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _adRepository.DeleteAsync(id, cancellationToken);
    }
}
