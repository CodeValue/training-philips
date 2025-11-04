using System;

namespace OrganLens.Analysis
{
    /// <summary>
    /// Very simple confidence heuristic: thinner slices = more detail = more confidence.
    /// It does NOT look at the image content.
    /// </summary>
    public static class ConfidenceModel
    {
        public const double Min = 0.30;
        public const double Max = 0.99;

        public static double FromSliceThickness(double sliceThicknessMm)
        {
            var raw = 1.0 - (sliceThicknessMm - 1.0) * 0.08;
            if (raw < Min) raw = Min;
            if (raw > Max) raw = Max;
            return Math.Round(raw, 2);
        }

        public static double ForSeries(ImageSeries series)
        {
            var thickness = series.Frames != null && series.Frames.Count > 0
                ? series.Frames[0].SliceThicknessMm
                : 1.0;
            return FromSliceThickness(thickness);
        }
    }
}
