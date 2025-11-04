using System;

namespace OrganLens.Analysis.Calculators
{
    /// <summary>
    /// Ellipsoid approximation: V = L * W * D * PI / 6.
    /// </summary>
    public static class KidneyVolumeCalculator
    {
        /// <returns>Volume in mL, rounded to 1 decimal.</returns>
        public static double CalculateMl(double lengthMm, double widthMm, double depthMm)
        {
            if (lengthMm <= 0 || widthMm <= 0 || depthMm <= 0)
            {
                throw new ArgumentException("Kidney dimensions must be positive.");
            }

            var cubicMm = lengthMm * widthMm * depthMm * Math.PI / 6.0;
            return Math.Round(cubicMm / 1000.0, 1);
        }
    }
}
