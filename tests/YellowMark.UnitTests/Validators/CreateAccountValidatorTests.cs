using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Account;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="CreateAccountRequest"/> validator test.
/// </summary>
public class CreateAccountValidatorTests : BaseUnitTest
{
    // FirstName
    [Theory]
    [InlineData("Jack")]
    [InlineData("Blob")]
    public void ShouldCorrect_FirstName(string testName)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, testName)
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
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
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }

    [Fact]
    public void ShouldError_FirstNameLonger()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }

    [Fact]
    public void ShouldError_FirstNameEmpty()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, string.Empty)
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }

    [Theory]
    [InlineData("Jeff1")]
    [InlineData("Jeremy#")]
    public void ShouldError_FirstNameIncorrect(string testName)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, testName)
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.FirstName).Only();
    }

    // MiddleName
    [Theory]
    [InlineData("Jr.")]
    [InlineData("Jason-Jason")]
    public void ShouldCorrect_MiddleName(string testName)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, testName)
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
    }

    [Fact]
    public void ShouldError_MiddleNameLonger()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.MiddleName).Only();
    }

    // LastName
    [Theory]
    [InlineData("Black")]
    [InlineData("Awesome")]
    [InlineData("Blob-Awesome")]
    public void ShouldCorrect_LastName(string testName)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, testName)
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void ShouldError_LastNameNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, (string?)null)
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.LastName).Only();
    }

    [Fact]
    public void ShouldError_LastNameLonger()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.LastName).Only();
    }

    [Fact]
    public void ShouldError_LastNameEmpty()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, string.Empty)
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.LastName).Only();
    }

    [Theory]
    [InlineData("Jeff1")]
    [InlineData("Jeremy#")]
    public void ShouldError_LastNameIncorrect(string testName)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, testName)
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.LastName).Only();
    }

    // BirthDate
    [Fact]
    public void ShouldError_BirthDateNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Blob")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
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
            .With(x => x.FirstName, "Blob")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-1)))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.BirthDate).Only();
    }

    // Email
    [Theory]
    [InlineData("test@email.com")]
    [InlineData("blob_awesome_email@email.com")]
    public void ShouldCorrect_Email(string testEmail)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Blob")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, testEmail)
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("testemail.com")]
    [InlineData("Jeremy#")]
    public void ShouldError_EmailIncorrect(string testEmail)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, testEmail)
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    [Fact]
    public void ShouldError_EmailNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, (string?)null)
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    [Fact]
    public void ShouldError_EmailEmpty()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, string.Empty)
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Email).Only();
    }

    // Phone
    [Fact]
    public void ShouldCorrect_Phone()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Phone);
    }

    [Fact]
    public void ShouldError_PhoneNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, (string?)null)
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Phone).Only();
    }

    [Theory]
    [InlineData("1(234112)890-1675")]
    // [InlineData("1(234)8901-1675")]
    // [InlineData("1(234)890-16751")]
    // [InlineData("1111(234)890-1675")]
    public void ShouldError_PhoneLonger(string testPhone)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, testPhone)
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Phone).Only();
    }

    [Fact]
    public void ShouldError_PhoneEmpty()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, string.Empty)
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Phone).Only();
    }

    [Theory]
    [InlineData("12348901675")]
    [InlineData("1(234)8901675")]
    // [InlineData("(234)890-1675")]
    // [InlineData("1234)890-1675")]
    // [InlineData("1(234890-1675")]
    public void ShouldError_PhoneIncorrect(string testPhone)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, testPhone)
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Phone).Only();
    }

    // Password
    [Theory]
    [InlineData("Password123$")]
    [InlineData("passWord1%@")]
    [InlineData("PASSWOrd123$")]
    public void ShouldCorrect_Password(string testPassword)
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, testPassword)
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void ShouldError_PasswordNull()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, (string?)null)
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordEmpty()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, string.Empty)
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordShort()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, "Pass1$")
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutUppercase()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, "password123$")
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutLowercase()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, "PASSWORD123$")
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutDigits()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, "password$")
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }

    [Fact]
    public void ShouldError_PasswordWithoutSpecialCharacters()
    {
        var createUserRequest = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.FirstName, "Jack")
            .With(x => x.MiddleName, "Jr.")
            .With(x => x.LastName, "Awesome")
            .With(x => x.Email, "test@email.com")
            .With(x => x.Phone, "1(234)890-1675")
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .With(x => x.Password, "Password123")
            .Create();
        var sut = new CreateAccountValidator();

        var result = sut.TestValidate(createUserRequest);

        result.ShouldHaveValidationErrorFor(x => x.Password).Only();
    }
}