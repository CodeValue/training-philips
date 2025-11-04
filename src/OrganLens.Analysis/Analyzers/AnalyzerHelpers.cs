using System;

namespace OrganLens.Analysis.Analyzers
{
    internal static class AnalyzerHelpers
    {
        public static double RequireMetadata(ImageSeries series, string key)
        {
            double value;
            if (series.Metadata == null || !series.Metadata.TryGetValue(key, out value))
            {
                throw new InvalidOperationException("Series " + series.SeriesId + " is missing metadata '" + key + "'.");
            }

            return value;
        }
    }
}
