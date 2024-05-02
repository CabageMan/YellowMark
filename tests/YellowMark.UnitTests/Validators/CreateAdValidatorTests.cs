using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Ads;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="CreateAdRequest"/> validator test.
/// </summary>
public class CreateAdValidatorTests : BaseUnitTest
{
    // Title
    [Theory]
    [InlineData("Fishing boat.")]
    [InlineData("Vintage writing desk from 1984.")]
    [InlineData("Awesome retro auto. \nLimited edition.")]
    public void ShouldCorrect_Title(string testTitle)
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Title, testTitle)
            .With(x => x.Price, 1000)
            .With(x => x.CurrencyId, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void ShouldError_TitleNull()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Title, (string?)null)
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Title).Only();
    }

    [Fact]
    public void ShouldError_TitleEmpty()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Title, string.Empty)
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Title).Only();
    }

    [Fact]
    public void ShouldError_TitleLonger()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Title, new string('a', 257))
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Title).Only();
    }

    // Description
    [Theory]
    [InlineData("Fishing boat. Is the funiest thing I have in my life.")]
    [InlineData("Vintage writing desk from 1984. Nobody had written at this desk till 1997.")]
    [InlineData("Awesome retro auto. \nLimited edition.\nOnly today.\nOnly for you.\bBuy \bit \nright \bNOW!")]
    public void ShouldCorrect_Description(string testDescription)
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Description, testDescription)
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void ShouldError_DescriptionNull()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Description, (string?)null)
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Description).Only();
    }

    [Fact]
    public void ShouldError_DescriptionEmpty()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Description, string.Empty)
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Description).Only();
    }

    [Fact]
    public void ShouldError_DescriptionLonger()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Description, new string('a', 2501))
            .With(x => x.Price, 1000)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Description).Only();
    }

    // Price
    public static TheoryData<decimal> DecimalData =>
        new() { 3.3M, decimal.MaxValue, 0 };

    [Theory]
    [MemberData(nameof(DecimalData))]
    public void ShouldCorrect_Price(decimal? testPrice)
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, testPrice)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void ShouldError_PriceNegative()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, -3)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.Price).Only();
    }

    // Currency Id
    public static TheoryData<Guid?> GuidData =>
        new() { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), null };

    [Theory]
    [MemberData(nameof(GuidData))]
    public void ShouldCorrect_CurrencyId(Guid? testGuid)
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.CurrencyId, testGuid)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.CurrencyId);
    }

    // Owner Id
    [Fact]
    public void ShouldCorrect_OwnerId()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.OwnerId, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.OwnerId);
    }

    [Fact]
    public void ShouldError_OwnerIdNull()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.OwnerId, (Guid?)null)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.OwnerId).Only();
    }

    [Fact]
    public void ShouldError_OwnerIdEmpty()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.OwnerId, Guid.Empty)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.OwnerId).Only();
    }

    // SubcategoryId Id
    [Fact]
    public void ShouldCorrect_SubcategoryId()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.SubcategoryId, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.SubcategoryId);
    }

    [Fact]
    public void ShouldError_SubcategoryIdNull()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.SubcategoryId, (Guid?)null)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.SubcategoryId).Only();
    }

    [Fact]
    public void ShouldError_SubcategoryIdEmpty()
    {
        var createAdRequest = Fixture
            .Build<CreateAdRequest>()
            .With(x => x.Price, 1000)
            .With(x => x.SubcategoryId, Guid.Empty)
            .Create();
        var sut = new CreateAdValidator();

        var result = sut.TestValidate(createAdRequest);

        result.ShouldHaveValidationErrorFor(x => x.SubcategoryId).Only();
    }
}
