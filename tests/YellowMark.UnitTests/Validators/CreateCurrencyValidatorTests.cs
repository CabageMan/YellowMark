using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Currnecies;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="CreateCurrencyRequest"/> validator test.
/// </summary>
public class CreateCurrencyValidatorTests : BaseUnitTest
{
    // AlphabeticCode
    [Theory]
    [InlineData("BHD")]
    [InlineData("LYD")]
    [InlineData("ZAR")]
    public void ShouldCorrect_AlphabeticCode(string testCode)
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, testCode)
            .With(x => x.NumericCode, 333)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.AlphabeticCode);
    }

    [Fact]
    public void ShouldError_AlphabeticCodeNull()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, (string?)null)
            .With(x => x.NumericCode, 333)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.AlphabeticCode).Only();
    }

    [Fact]
    public void ShouldError_AlphabeticCodeEmpty()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, string.Empty)
            .With(x => x.NumericCode, 333)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.AlphabeticCode).Only();
    }

    [Fact]
    public void ShouldError_AlphabeticCodeShorter()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, new string('A', 2))
            .With(x => x.NumericCode, 333)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.AlphabeticCode).Only();
    }

    [Fact]
    public void ShouldError_AlphabeticCodeLonger()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, new string('A', 4))
            .With(x => x.NumericCode, 333)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.AlphabeticCode).Only();
    }

    [Theory]
    [InlineData("BH4")]
    [InlineData("L#D")]
    [InlineData("%AR")]
    public void ShoulError_AlphabeticCodeIncorrect(string testCode)
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, testCode)
            .With(x => x.NumericCode, 333)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.AlphabeticCode).Only();
    }

    // NumericCode
    [Theory]
    [InlineData(1)]
    [InlineData(22)]
    [InlineData(333)]
    public void ShouldCorrect_NumericCode(int testCode)
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, "AAA")
            .With(x => x.NumericCode, testCode)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.NumericCode);
    }

    [Fact]
    public void ShouldError_NumericCodeNull()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, "AAA")
            .With(x => x.NumericCode, (int?)null)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.NumericCode).Only();
    }

    [Fact]
    public void ShouldError_NumericCodeZero()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, "AAA")
            .With(x => x.NumericCode, 0)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.NumericCode).Only();
    }

    [Fact]
    public void ShouldError_NumericCodeNegative()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, "AAA")
            .With(x => x.NumericCode, -1)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.NumericCode).Only();
    }

    [Fact]
    public void ShouldError_NumericCodeGreater()
    {
        var createCurrencyRequest = Fixture
            .Build<CreateCurrencyRequest>()
            .With(x => x.AlphabeticCode, "AAA")
            .With(x => x.NumericCode, 1001)
            .Create();
        var sut = new CreateCurrencyValidator();

        var result = sut.TestValidate(createCurrencyRequest);

        result.ShouldHaveValidationErrorFor(x => x.NumericCode).Only();
    }
}
