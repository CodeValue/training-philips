using OrganLens.Analysis;
using Xunit;

namespace OrganLens.Api.Tests.Unit;

public class ConfidenceModelTests
{
    [Theory]
    [InlineData(1.0, 0.99)]
    [InlineData(1.25, 0.98)]
    [InlineData(2.0, 0.92)]
    [InlineData(5.0, 0.68)]
    [InlineData(6.0, 0.60)]
    [InlineData(8.0, 0.44)]
    [InlineData(20.0, 0.30)]
    public void FromSliceThickness_ThinnerIsMoreConfident(double sliceMm, double expected)
    {
        Assert.Equal(expected, ConfidenceModel.FromSliceThickness(sliceMm), 2);
    }
}
