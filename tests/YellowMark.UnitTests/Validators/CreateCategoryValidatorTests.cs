using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Categories;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="CreateCategoryRequest"/> validator test.
/// </summary>
public class CreateCategoryValidatorTests : BaseUnitTest
{
    // Title
    [Theory]
    [InlineData("Fishing boats")]
    [InlineData("Steam locomotivs")]
    [InlineData("Retro auto")]
    public void ShouldCorrect_Name(string testName)
    {
        var createCategoryRequest = Fixture
            .Build<CreateCategoryRequest>()
            .With(x => x.Name, testName)
            .Create();
        var sut = new CreateCategoryValidator();

        var result = sut.TestValidate(createCategoryRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldError_NameNull()
    {
        var createCategoryRequest = Fixture
            .Build<CreateCategoryRequest>()
            .With(x => x.Name, (string?)null)
            .Create();
        var sut = new CreateCategoryValidator();

        var result = sut.TestValidate(createCategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    [Fact]
    public void ShouldError_NameEmpty()
    {
        var createCategoryRequest = Fixture
            .Build<CreateCategoryRequest>()
            .With(x => x.Name, string.Empty)
            .Create();
        var sut = new CreateCategoryValidator();

        var result = sut.TestValidate(createCategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    [Fact]
    public void ShouldError_NameLonger()
    {
        var createCategoryRequest = Fixture
            .Build<CreateCategoryRequest>()
            .With(x => x.Name, new string('a', 257))
            .Create();
        var sut = new CreateCategoryValidator();

        var result = sut.TestValidate(createCategoryRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }
}
