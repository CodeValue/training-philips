using Microsoft.Extensions.Options;
using OrganLens.Analysis;
using OrganLens.Api.Services;
using Xunit;

namespace OrganLens.Api.Tests.Unit;

public class ReviewPolicyTests
{
    private static ReviewPolicy CreatePolicy(double threshold = 0.70)
    {
        return new ReviewPolicy(Options.Create(new AnalysisOptions { ReviewThreshold = threshold }));
    }

    private static Finding FindingWithConfidence(double confidence)
    {
        return new Finding { Code = "TEST", Confidence = confidence };
    }

    [Fact]
    public void ConfidenceBelowThreshold_RequiresReview()
    {
        var policy = CreatePolicy();

        Assert.True(policy.RequiresReview(new[] { FindingWithConfidence(0.68) }));
    }

    [Fact]
    public void ConfidenceEqualToThreshold_RequiresReview()
    {
        var policy = CreatePolicy();

        Assert.True(policy.RequiresReview(new[] { FindingWithConfidence(0.70) }));
    }

    [Fact]
    public void ConfidenceAboveThreshold_DoesNotRequireReview()
    {
        var policy = CreatePolicy();

        Assert.False(policy.RequiresReview(new[] { FindingWithConfidence(0.92) }));
    }

    [Fact]
    public void NoFindings_DoesNotRequireReview()
    {
        var policy = CreatePolicy();

        Assert.False(policy.RequiresReview(Array.Empty<Finding>()));
    }

    [Fact]
    public void DefaultThreshold_Is070()
    {
        Assert.Equal(0.70, new AnalysisOptions().ReviewThreshold);
    }
}
