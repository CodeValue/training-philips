namespace OrganLens.Analysis
{
    /// <summary>
    /// One 2D slice. HU values are stored row by row (Width * Height).
    /// </summary>
    public class ImageFrame
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double PixelSpacingMm { get; set; }
        public double SliceThicknessMm { get; set; }
        public short[] HuValues { get; set; }
    }
}
