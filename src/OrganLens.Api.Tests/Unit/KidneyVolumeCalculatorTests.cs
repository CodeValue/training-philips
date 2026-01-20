using OrganLens.Analysis.Calculators;
using Xunit;

namespace OrganLens.Api.Tests.Unit;

public class KidneyVolumeCalculatorTests
{
    [Theory]
    [InlineData(110, 50, 45, 129.6)]
    [InlineData(130, 60, 55, 224.6)]
    [InlineData(95, 45, 40, 89.5)]
    public void CalculateMl_UsesEllipsoidFormula(double length, double width, double depth, double expectedMl)
    {
        Assert.Equal(expectedMl, KidneyVolumeCalculator.CalculateMl(length, width, depth), 1);
    }

    [Fact]
    public void CalculateMl_RejectsZeroDimension()
    {
        Assert.Throws<ArgumentException>(() => KidneyVolumeCalculator.CalculateMl(110, 0, 45));
    }
}
