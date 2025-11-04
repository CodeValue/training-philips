using System;

namespace OrganLens.Analysis.Calculators
{
    public static class EjectionFractionCalculator
    {
        /// <summary>
        /// EF = (EDV - ESV) / EDV * 100.
        /// </summary>
        /// <returns>Ejection fraction in percent (0..100), rounded to 1 decimal.</returns>
        public static double Calculate(double edvMl, double esvMl)
        {
            if (edvMl <= 0) throw new ArgumentException("EDV must be positive.", "edvMl");
            if (esvMl < 0) throw new ArgumentException("ESV cannot be negative.", "esvMl");
            if (esvMl > edvMl) throw new ArgumentException("ESV cannot exceed EDV.", "esvMl");

            return Math.Round((edvMl - esvMl) / edvMl * 100.0, 1);
        }
    }
}
