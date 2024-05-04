using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Comments;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="CreateCommentRequest"/> validator test.
/// </summary>
public class CreateCommentValidatorTests : BaseUnitTest
{
    // Text
    [Theory]
    [InlineData("Nice fishing boat!")]
    [InlineData("I havent seen a Vintage writing desk like this since 1984.")]
    [InlineData("Awesome retro auto. \nLimited edition.\nI need it!")]
    public void ShouldCorrect_Text(string testText)
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.Text, testText)
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Text);
    }

    [Fact]
    public void ShouldError_TextNull()
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.Text, (string?)null)
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Text).Only();
    }

    [Fact]
    public void ShouldError_TextEmpty()
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.Text, string.Empty)
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Text).Only();
    }

    [Fact]
    public void ShouldError_TextLonger()
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.Text, new string('a', 1025))
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.Text).Only();
    }

    // Ad Id
    [Fact]
    public void ShouldCorrect_AdId()
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.AdId, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.AdId);
    }

    [Fact]
    public void ShouldError_AdIdNull()
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.AdId, (Guid?)null)
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.AdId).Only();
    }

    [Fact]
    public void ShouldError_AdIdEmpty()
    {
        var createCommentRequest = Fixture
            .Build<CreateCommentRequest>()
            .With(x => x.AdId, Guid.Empty)
            .Create();
        var sut = new CreateCommentValidator();

        var result = sut.TestValidate(createCommentRequest);

        result.ShouldHaveValidationErrorFor(x => x.AdId).Only();
    }
}
