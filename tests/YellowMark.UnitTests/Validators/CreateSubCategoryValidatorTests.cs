using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Subcategories;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="CreateSubcategoryRequest"/> validator test.
/// </summary>
public class CreateSubCategoryValidatorTests : BaseUnitTest
{
    // Title
    [Theory]
    [InlineData("Fishing boats")]
    [InlineData("Steam locomotives")]
    [InlineData("Retro auto")]
    public void ShouldCorrect_Name(string testName)
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.Name, testName)
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldError_NameNull()
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.Name, (string?)null)
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    [Fact]
    public void ShouldError_NameEmpty()
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.Name, string.Empty)
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    [Fact]
    public void ShouldError_NameLonger()
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.Name, new string('a', 257))
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    // Category Id
    [Fact]
    public void ShouldCorrect_CategoryId()
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.CategoryId, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
    }

    [Fact]
    public void ShouldError_CategoryIdNull()
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.CategoryId, (Guid?)null)
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.CategoryId).Only();
    }

    [Fact]
    public void ShouldError_CategoryIdEmpty()
    {
        var createSubcategoryRequest = Fixture
            .Build<CreateSubcategoryRequest>()
            .With(x => x.CategoryId, Guid.Empty)
            .Create();
        var sut = new CreateSubcategoryValidator();

        var result = sut.TestValidate(createSubcategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.CategoryId).Only();
    }
}