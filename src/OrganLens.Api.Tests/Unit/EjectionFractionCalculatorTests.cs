using OrganLens.Analysis.Calculators;
using Xunit;

namespace OrganLens.Api.Tests.Unit;

public class EjectionFractionCalculatorTests
{
    [Theory]
    [InlineData(120, 48, 60.0)]
    [InlineData(150, 90, 40.0)]
    public void Calculate_ReturnsPercent(double edv, double esv, double expected)
    {
        Assert.Equal(expected, EjectionFractionCalculator.Calculate(edv, esv), 1);
    }

    [Fact]
    public void Calculate_RejectsZeroEdv()
    {
        Assert.Throws<ArgumentException>(() => EjectionFractionCalculator.Calculate(0, 0));
    }

    [Fact]
    public void Calculate_RejectsEsvGreaterThanEdv()
    {
        Assert.Throws<ArgumentException>(() => EjectionFractionCalculator.Calculate(100, 120));
    }
}
