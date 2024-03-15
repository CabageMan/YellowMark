using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Currencies.Repositories;
using YellowMark.Contracts.Currnecies;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Currency.Repository;


/// <inheritdoc />
public class CurrencyRepository : ICurrencyRepository
{
    /*
    private readonly IWriteOnlyRepository<Domain.Currencies.Entity.Currency> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Currencies.Entity.Currency> _readOnlyRepository;

    /// <summary>
    /// Init CurrencyRepository (<see cref="ICurrencyRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public CurrencyRepository (
        IWriteOnlyRepository<Domain.Currencies.Entity.Currency> writeOnlyRepository,
        IReadOnlyRepository<Domain.Currencies.Entity.Currency> readOnlyRepository)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
    }
    */

    /// <inheritdoc />
    public async Task<IEnumerable<CurrencyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var currencies = MockList();
        return await Task.Run(() => currencies.Select(currency => new CurrencyDto
        {
            AlphabeticCode = currency.AlphabeticCode,
            NumericCode = currency.NumericCode
        }));
        /*
        var currencies = await _readOnlyRepository.GetAll().ToListAsync(cancellationToken);

        return currencies.Select(currency => new CurrencyDto
        {
            AlphabeticCode = currency.AlphabeticCode,
            NumericCode = currency.NumericCode
        });
        */
    }

    // Mock Currency Data
    private static List<Domain.Currencies.Entity.Currency> MockList()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                AlphabeticCode = "RUB",
                NumericCode = 643
            },
            new()
            {
                Id = Guid.NewGuid(),
                AlphabeticCode = "RWF",
                NumericCode = 646
            },
            new()
            {
                Id = Guid.NewGuid(),
                AlphabeticCode = "RON",
                NumericCode = 946
            },
        ];
    }
}
