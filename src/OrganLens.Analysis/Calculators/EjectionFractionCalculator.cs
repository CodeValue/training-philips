using System;

namespace OrganLens.Analysis.Calculators
{
    public static class EjectionFractionCalculator
    {
        /// <summary>
        /// EF = (EDV - ESV) / EDV.
        /// </summary>
        /// <returns>Ejection fraction (0..1), rounded to 3 decimals.</returns>
        public static double Calculate(double edvMl, double esvMl)
        {
            if (edvMl <= 0) throw new ArgumentException("EDV must be positive.", "edvMl");
            if (esvMl < 0) throw new ArgumentException("ESV cannot be negative.", "esvMl");
            if (esvMl > edvMl) throw new ArgumentException("ESV cannot exceed EDV.", "esvMl");

            return Math.Round((edvMl - esvMl) / edvMl, 3);
        }
    }
}
