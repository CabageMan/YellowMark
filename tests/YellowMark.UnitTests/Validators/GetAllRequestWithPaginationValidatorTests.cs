using AutoFixture;
using FluentValidation.TestHelper;
using YellowMark.AppServices.Validators;
using YellowMark.Contracts.Pagination;

namespace YellowMark.UnitTests.Validators;

/// <summary>
/// <see cref="GetAllRequestWithPagination"/> validator test.
/// </summary>
public class GetAllRequestWithPaginationValidatorTests : BaseUnitTest
{
    // Page number.
    [Theory]
    [InlineData(1)]
    [InlineData(22)]
    [InlineData(333)]
    public void ShouldCorrect_PageNumber(int testPageNumber)
    {
        var getAllRequest = Fixture
            .Build<GetAllRequestWithPagination>()
            .With(x => x.PageNumber, testPageNumber)
            .Create();
        var sut = new GetAllRequestWithPaginationValidator();

        var result = sut.TestValidate(getAllRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.PageNumber);
    }

    [Fact]
    public void ShouldError_PageNumberNull()
    {
        var getAllRequest = Fixture
            .Build<GetAllRequestWithPagination>()
            .With(x => x.PageNumber, (int?)null)
            .Create();
        var sut = new GetAllRequestWithPaginationValidator();

        var result = sut.TestValidate(getAllRequest);

        result.ShouldHaveValidationErrorFor(x => x.PageNumber).Only();
    }

    [Fact]
    public void ShouldError_PageNumberNegative()
    {
        var getAllRequest = Fixture
            .Build<GetAllRequestWithPagination>()
            .With(x => x.PageNumber, -1)
            .Create();
        var sut = new GetAllRequestWithPaginationValidator();

        var result = sut.TestValidate(getAllRequest);

        result.ShouldHaveValidationErrorFor(x => x.PageNumber).Only();
    }

    // Batch size.
    [Theory]
    [InlineData(1)]
    [InlineData(22)]
    [InlineData(333)]
    public void ShouldCorrect_BatchSize(int testBatchSize)
    {
        var getAllRequest = Fixture
            .Build<GetAllRequestWithPagination>()
            .With(x => x.BatchSize, testBatchSize)
            .Create();
        var sut = new GetAllRequestWithPaginationValidator();

        var result = sut.TestValidate(getAllRequest);

        result.ShouldNotHaveValidationErrorFor(x => x.BatchSize);
    }

    [Fact]
    public void ShouldError_BatcSizeNull()
    {
        var getAllRequest = Fixture
            .Build<GetAllRequestWithPagination>()
            .With(x => x.BatchSize, (int?)null)
            .Create();
        var sut = new GetAllRequestWithPaginationValidator();

        var result = sut.TestValidate(getAllRequest);

        result.ShouldHaveValidationErrorFor(x => x.BatchSize).Only();
    }

    [Fact]
    public void ShouldError_BatchSizeNegative()
    {
        var getAllRequest = Fixture
            .Build<GetAllRequestWithPagination>()
            .With(x => x.BatchSize, -1)
            .Create();
        var sut = new GetAllRequestWithPaginationValidator();

        var result = sut.TestValidate(getAllRequest);

        result.ShouldHaveValidationErrorFor(x => x.BatchSize).Only();
    }
}
