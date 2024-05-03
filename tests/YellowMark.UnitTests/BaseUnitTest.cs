using AutoFixture;

namespace YellowMark.UnitTests;

/// <summary>
/// Base unit test class with presetup.
/// </summary>
public abstract class BaseUnitTest
{
    /// <summary>
    /// Fixture instance <see cref="Fixture"/>
    /// </summary>
    protected readonly Fixture Fixture;

    /// <summary>
    /// Operation cancellation token instance <see cref="CancellationToken"/>
    /// </summary>
    protected readonly CancellationToken CancellationToken;

    /// <summary>
    /// Constructor for BaseUnitTest.
    /// </summary>
    protected BaseUnitTest()
    {
        Fixture = new Fixture();
        CancellationToken = new CancellationTokenSource().Token;
    }
}
