using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Account;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// CreateAccountRequest validator test.
/// </summary>
public class CreateAccountValidatorTests : BaseUnitTest
{
    [Theory]
    [InlineData("Jack")]
    [InlineData("Blob")]
    public void ShouldCorrect_FirstName(string testName)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, testName)
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void ShouldError_FirstNameNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, (string?)null)
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }

    [Fact]
    public void ShouldError_BirthDateNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, (DateOnly?)null)
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.BirthDate).Only();
    }

    [Fact]
    public void ShouldError_Under18()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-1)))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.BirthDate).Only();
    }
}