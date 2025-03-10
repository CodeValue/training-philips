using System;
using System.Collections.Generic;

namespace OrganLens.Legacy.Core
{
    public class StudyRecord
    {
        public StudyRecord()
        {
            Organs = new List<string>();
            Metrics = new Dictionary<string, double>();
            Patient = new PatientInfo();
        }

        public string StudyId { get; set; }
        public PatientInfo Patient { get; set; }
        public DateTime StudyDate { get; set; }
        public string Modality { get; set; }
        public List<string> Organs { get; set; }

        // MeanHu, LengthMm, WidthMm, DepthMm, EdvMl, EsvMl, Nodule1Mm..., ThicknessMm (spleen)
        public Dictionary<string, double> Metrics { get; set; }

        public double SliceThicknessMm { get; set; }
    }
}
