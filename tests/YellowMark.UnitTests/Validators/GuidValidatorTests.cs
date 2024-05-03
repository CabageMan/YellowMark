using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="Guid"/> validator test.
/// </summary>
public class GuidValidatorTests
{
    [Fact]
    public void ShouldCorrect_Guid()
    {
        var guid = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var sut = new GuidValidator();

        var result = sut.TestValidate(guid);

        result.ShouldNotHaveValidationErrorFor(x => x);
    }

    [Fact]
    public void ShouldError_GuidEmpty()
    {
        var guid = Guid.Empty;
        var sut = new GuidValidator();

        var result = sut.TestValidate(guid);

        result.ShouldHaveValidationErrorFor(x => x).Only();
    }
}
