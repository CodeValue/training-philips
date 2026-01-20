using OrganLens.Analysis;

namespace OrganLens.Api.Data;

/// <summary>
/// SYNTHETIC studies. Same 8 studies as data/legacy-studies.xml (the legacy file also has SYN-0009, a spleen study).
/// </summary>
public static class SeedData
{
    private const int FrameSize = 16;
    private const int FramesPerSeries = 3;

    public static List<StudyEntity> Create()
    {
        return new List<StudyEntity>
        {
            Study("SYN-0001", "P-1001", "Test Patient 01", "CT", new DateTime(2026, 1, 12),
                Series("SYN-0001-S1", Organ.Liver, 1.0, 55)),

            Study("SYN-0002", "P-1002", "Test Patient 02", "CT", new DateTime(2026, 2, 3),
                Series("SYN-0002-S1", Organ.Kidney, 1.25, 30, ("LengthMm", 110), ("WidthMm", 50), ("DepthMm", 45))),

            Study("SYN-0003", "P-1003", "Test Patient 03", "MR", new DateTime(2026, 2, 20),
                Series("SYN-0003-S1", Organ.Heart, 8.0, 40, ("EdvMl", 120), ("EsvMl", 48))),

            Study("SYN-0004", "P-1004", "Test Patient 04", "CT", new DateTime(2026, 3, 8),
                Series("SYN-0004-S1", Organ.Lung, 1.0, -850, ("Nodule1Mm", 4), ("Nodule2Mm", 7))),

            Study("SYN-0005", "P-1005", "Test Patient 05", "CT", new DateTime(2026, 3, 21),
                Series("SYN-0005-S1", Organ.Liver, 5.0, 32),
                Series("SYN-0005-S2", Organ.Kidney, 5.0, 30, ("LengthMm", 130), ("WidthMm", 60), ("DepthMm", 55))),

            Study("SYN-0006", "P-1006", "Test Patient 06", "MR", new DateTime(2026, 4, 2),
                Series("SYN-0006-S1", Organ.Heart, 6.0, 40, ("EdvMl", 150), ("EsvMl", 90))),

            Study("SYN-0007", "P-1007", "Test Patient 07", "CT", new DateTime(2026, 4, 15),
                Series("SYN-0007-S1", Organ.Lung, 5.0, -850, ("Nodule1Mm", 3), ("Nodule2Mm", 6), ("Nodule3Mm", 9))),

            Study("SYN-0008", "P-1008", "Test Patient 08", "CT", new DateTime(2026, 5, 1),
                Series("SYN-0008-S1", Organ.Kidney, 2.0, 30, ("LengthMm", 95), ("WidthMm", 45), ("DepthMm", 40))),
        };
    }

    private static StudyEntity Study(string id, string patientRef, string patientName, string modality, DateTime date, params ImageSeries[] series)
    {
        return new StudyEntity
        {
            Id = id,
            PatientRef = patientRef,
            PatientName = patientName,
            Modality = modality,
            StudyDate = date,
            Series = series.ToList()
        };
    }

    private static ImageSeries Series(string seriesId, Organ organ, double sliceThicknessMm, int baseHu, params (string Key, double Value)[] metadata)
    {
        var series = new ImageSeries { SeriesId = seriesId, Organ = organ };

        for (var f = 0; f < FramesPerSeries; f++)
        {
            series.Frames.Add(Frame(sliceThicknessMm, baseHu));
        }

        foreach (var (key, value) in metadata)
        {
            series.Metadata[key] = value;
        }

        return series;
    }

    // Deterministic fake pixels: value(i) = base + ((i * 37) % 21) - 10. Mean is base - 0.03.
    private static ImageFrame Frame(double sliceThicknessMm, int baseHu)
    {
        var pixels = new short[FrameSize * FrameSize];
        for (var i = 0; i < pixels.Length; i++)
        {
            pixels[i] = (short)(baseHu + ((i * 37) % 21) - 10);
        }

        return new ImageFrame
        {
            Width = FrameSize,
            Height = FrameSize,
            PixelSpacingMm = 0.7,
            SliceThicknessMm = sliceThicknessMm,
            HuValues = pixels
        };
    }
}
