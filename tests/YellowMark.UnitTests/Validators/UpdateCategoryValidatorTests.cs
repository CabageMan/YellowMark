using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Categories;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="UpdateCategoryValidatorTests"/> validator test.
/// </summary>
public class UpdateCategoryValidatorTests : BaseUnitTest
{
    // Name
    [Theory]
    [InlineData("Health")]
    [InlineData("Auto")]
    [InlineData("Food")]
    public void ShouldCorrect_Name(string testName)
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Name, testName)
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldError_NameNull()
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Name, (string?)null)
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    [Fact]
    public void ShouldError_NameEmpty()
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Name, string.Empty)
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    [Fact]
    public void ShouldError_NameLonger()
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Name, new string('a', 257))
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Name).Only();
    }

    // Category Id
    [Fact]
    public void ShouldCorrect_Id()
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Id, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void ShouldError_IdNull()
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Id, (Guid?)null)
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Id).Only();
    }

    [Fact]
    public void ShouldError_IdEmpty()
    {
        var updateCommentRequest = Fixture
            .Build<UpdateCategoryRequest>()
            .With(x => x.Id, Guid.Empty)
            .Create();
        var sut = new UpdateCategoryValidator();

        var result = sut.TestValidate(updateCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Id).Only();
    }
}
