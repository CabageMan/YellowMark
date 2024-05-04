using AutoMapper;
using YellowMark.AppServices.Accounts.Services;
using YellowMark.AppServices.Ads.Exceptions;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.AppServices.Ads.Specifications;
using YellowMark.AppServices.Categories.Services;
using YellowMark.AppServices.Currencies.Services;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Pagination;
using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.UserRoles;

namespace YellowMark.AppServices.Ads.Services;

/// <inheritdoc cref="IAdService"/>
public class AdService : IAdService
{
    private readonly IAdRepository _adRepository;
    private readonly IAccountService _accountService;
    private readonly ICurrencyService _currencyService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="AdService"/> instance.
    /// </summary>
    /// <param name="adRepository">Ad repository <see cref="IAdRepository"/></param>
    /// <param name="accountService">Account service <see cref="IAccountService"/></param>
    /// <param name="currencyService">Account service <see cref="ICurrencyService"/></param>
    /// <param name="categoryService">Account service <see cref="ICategoryService"/></param>
    /// <param name="mapper">Ads mapper.</param>
    public AdService(
        IAdRepository adRepository,
        IAccountService accountService,
        ICurrencyService currencyService,
        ICategoryService categoryService,
        IMapper mapper)
    {
        _adRepository = adRepository;
        _accountService = accountService;
        _currencyService = currencyService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAdAsync(CreateAdRequest request, CancellationToken cancellationToken)
    {
        if (request.CurrencyId != null)
        {
            var currencyExists = await _currencyService.CurrencyExistsWithId((Guid)request.CurrencyId, cancellationToken);
            AdOperationException.ThrowIfFalse(currencyExists, $"There is no currency with id: {request.CurrencyId}.");
        }

        var categoryExists = await _categoryService.CategoryExistsWithId(request.CategoryId, cancellationToken);
        AdOperationException.ThrowIfFalse(categoryExists, $"There is no category with id: {request.CategoryId}.");

        var currentUserInfo = await _accountService.GetAccountInfoAssync(cancellationToken);
        AdOperationException.ThrowIfNull(currentUserInfo, "Could not get user info for ad creation.");

        var entity = _mapper.Map<CreateAdRequest, Ad>(request);
        entity.UserInfoId = currentUserInfo.Id;

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
    public Task<bool> AdExistsWithId(Guid id, CancellationToken cancellationToken)
    {
        return _adRepository.ExistsWithId(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<AdDto> UpdateAdAsync(Guid id, CreateAdRequest request, CancellationToken cancellationToken)
    {
        var currentUserInfo = await _accountService.GetAccountInfoAssync(cancellationToken);
        AdOperationException.ThrowIfNull(currentUserInfo, "Could not get user info for ad update.");

        var currentEntity = await _adRepository.GetByIdAsync(id, cancellationToken);
        AdNotFoundException.ThrowIfNull(currentEntity, "Could not get ad for update.");

        if (currentUserInfo.Id != currentEntity.UserInfoId)
        {
            throw new AdPermissionsDeniedException("Current user can not update this ad.");
        }

        if (request.CurrencyId != null)
        {
            var currencyExists = await _currencyService.CurrencyExistsWithId((Guid)request.CurrencyId, cancellationToken);
            AdOperationException.ThrowIfFalse(currencyExists, $"There is no currency with id: {request.CurrencyId}.");
        }

        var categoryExists = await _categoryService.CategoryExistsWithId(request.CategoryId, cancellationToken);
        AdOperationException.ThrowIfFalse(categoryExists, $"There is no category with id: {request.CategoryId}.");

        var updatedEntity = _mapper.Map<CreateAdRequest, Ad>(request);
        updatedEntity.Id = id;
        updatedEntity.CreatedAt = currentEntity.CreatedAt;

        await _adRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<Ad, AdDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteAdByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var currentUserInfo = await _accountService.GetAccountInfoAssync(cancellationToken);
        AdOperationException.ThrowIfNull(currentUserInfo, "Could not get user info for ad deletion.");

        var currentEntity = await _adRepository.GetByIdAsync(id, cancellationToken);
        AdNotFoundException.ThrowIfNull(currentEntity, "There is nothing to delete.");

        if (
            currentUserInfo.Id != currentEntity.UserInfoId ||
            currentUserInfo.UserRoles.Contains(UserRoles.Admin) ||
            currentUserInfo.UserRoles.Contains(UserRoles.SuperUser))
        {
            throw new AdPermissionsDeniedException("Current user can not delete this ad.");
        }

        await _adRepository.DeleteAsync(id, cancellationToken);
    }
}
