using System;

namespace OrganLens.Legacy.Core.Utils
{
    public static class MathHelper
    {
        public static double Round1(double value)
        {
            return Math.Round(value, 1);
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        // TODO: remove? used by the old DICOM importer (deleted in 2019)
        public static double MmToInch(double mm)
        {
            return mm / 25.4;
        }
    }
}
