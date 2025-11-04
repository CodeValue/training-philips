using System.Collections.Generic;

namespace OrganLens.Analysis
{
    public class ImageSeries
    {
        public ImageSeries()
        {
            Frames = new List<ImageFrame>();
            Metadata = new Dictionary<string, double>();
        }

        public string SeriesId { get; set; }
        public Organ Organ { get; set; }
        public List<ImageFrame> Frames { get; set; }

        /// <summary>
        /// Pre-computed values from the acquisition / segmentation step (e.g. LengthMm, EdvMl).
        /// </summary>
        public Dictionary<string, double> Metadata { get; set; }
    }
}
