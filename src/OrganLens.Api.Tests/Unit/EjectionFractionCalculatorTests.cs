using OrganLens.Analysis.Calculators;
using Xunit;

namespace OrganLens.Api.Tests.Unit;

public class EjectionFractionCalculatorTests
{
    [Theory]
    [InlineData(120, 48, 0.6)]
    [InlineData(150, 90, 0.4)]
    public void Calculate_ReturnsFraction(double edv, double esv, double expected)
    {
        Assert.Equal(expected, EjectionFractionCalculator.Calculate(edv, esv), 3);
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
