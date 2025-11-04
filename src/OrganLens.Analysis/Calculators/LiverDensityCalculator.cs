using System;
using System.Collections.Generic;

namespace OrganLens.Analysis.Calculators
{
    public static class LiverDensityCalculator
    {
        /// <returns>Mean density in Hounsfield units (HU) over all pixels of all frames, rounded to 1 decimal.</returns>
        public static double CalculateMeanHu(IEnumerable<ImageFrame> frames)
        {
            long sum = 0;
            long count = 0;

            foreach (var frame in frames)
            {
                if (frame.HuValues == null) continue;
                foreach (var hu in frame.HuValues)
                {
                    sum += hu;
                    count++;
                }
            }

            if (count == 0)
            {
                throw new InvalidOperationException("Series has no pixel data.");
            }

            return Math.Round((double)sum / count, 1);
        }
    }
}
