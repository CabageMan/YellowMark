using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Currencies.Entity;

namespace YellowMark.AppServices.Currencies.Specifications;

/// <summary>
/// Get Currency by Numeric or Alphabethic code implementation of the Specification.
/// </summary>
public class CurrencyByCodesSpecification : Specification<Currency>
{
    private readonly string _code;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="code">Target Currency param.</param>
    public CurrencyByCodesSpecification(string code)
    {
        _code = code;
    }

    /// <summary>
    /// Get Currency by code implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Currency, bool>> ToExpression()
    {
        if (Int32.TryParse(_code, out var numericCode))
        {
            return category => category.NumericCode == numericCode;
        }
        return category => category.AlphabeticCode == _code;
    }
}
