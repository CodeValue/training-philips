using Microsoft.Extensions.Options;
using OrganLens.Analysis;

namespace OrganLens.Api.Services;

/// <summary>
/// WI-1077: decides whether an analysis must be reviewed by a radiologist.
/// </summary>
public class ReviewPolicy
{
    private readonly AnalysisOptions _options;

    public ReviewPolicy(IOptions<AnalysisOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// True when any finding's confidence is at or below the configured threshold.
    /// No findings = nothing to review.
    /// </summary>
    public bool RequiresReview(IEnumerable<Finding> findings)
    {
        if (findings == null)
        {
            return false;
        }

        // Treat the threshold itself as low confidence to be on the safe side.
        return findings.Any(f => f.Confidence <= _options.ReviewThreshold);
    }
}
