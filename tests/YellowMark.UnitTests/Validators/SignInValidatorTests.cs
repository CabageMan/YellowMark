using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Account;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="SignInRequest"/> validator test.
/// </summary>
public class SignInValidatorTests : BaseUnitTest
{
    // Email
    [Theory]
    [InlineData("test@email.com")]
    [InlineData("blob_awesome_email@email.com")]
    public void ShouldCorrect_Email(string testEmail)
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, testEmail)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("testemail.com")]
    [InlineData("Jeremy#")]
    public void ShouldError_EmailIncorrect(string testEmail)
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, testEmail)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    [Fact]
    public void ShouldError_EmailNull()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, (string?)null)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    [Fact]
    public void ShouldError_EmailEmpty()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, string.Empty)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    // Password
    [Theory]
    [InlineData("Password123$")]
    [InlineData("passWord1%@")]
    [InlineData("PASSWOrd123$")]
    public void ShouldCorrect_Password(string testPassword)
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, testPassword)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void ShouldError_PasswordNull()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, (string?)null)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordEmpty()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, string.Empty)
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordShort()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, "Pass1$")
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutUppercase()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, "password123$")
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutLowercase()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, "PASSWORD123$")
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutDigits()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, "password$")
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutSpecialCharacters()
    {
        var signInRequest = Fixture
            .Build<SignInRequest>()
            .With(x => x.Email, "test@email.com")
            .With(x => x.Password, "Password123")
            .Create();
        var sut = new SigninValidator();

        var result = sut.TestValidate(signInRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }
}
