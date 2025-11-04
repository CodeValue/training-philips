namespace OrganLens.Analysis
{
    public interface IOrganAnalyzer
    {
        Organ Organ { get; }

        AnalysisResult Analyze(ImageSeries series);
    }
}
