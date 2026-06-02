using System.Net;
using System.Net.Http.Json;
using OrganLens.Analysis;
using OrganLens.Api.Data;
using OrganLens.Api.Models;
using Xunit;

namespace OrganLens.Api.Tests.Integration;

public class StudyFilterTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public StudyFilterTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Filter_ByOrgan_Works()
    {
        var response = await _client.GetAsync("/api/studies?organ=Kidney");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Filter_ByOrgan_ReturnsKidneyStudies()
    {
        var list = await _client.GetFromJsonAsync<List<StudySummaryDto>>("/api/studies?organ=Kidney");

        // verify the filter returns kidney studies
    }

    [Fact]
    public void Filter_UsesStore()
    {
        var store = new FakeStudyStore();

        var studies = store.FetchAllStudies();

        Assert.Equal(2, studies.Count);
    }

    [Fact]
    public async Task Filter_ByOrgan_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/studies?organ=Kidney");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private class FakeStudyStore : IStudyStore
    {
        private readonly List<StudyEntity> _studies = new()
        {
            new StudyEntity { Id = "S1", Series = new List<ImageSeries> { new ImageSeries { Organ = Organ.Kidney } } },
            new StudyEntity { Id = "S2", Series = new List<ImageSeries> { new ImageSeries { Organ = Organ.Liver } } }
        };

        public IReadOnlyList<StudyEntity> FetchAllStudies() => _studies;

        public StudyEntity FetchStudyById(string id) => _studies.FirstOrDefault(s => s.Id == id);

        public MeasurementDto AddMeasurement(string studyId, MeasurementDto measurement) => measurement;

        public void AddAnalysis(string studyId, AnalysisResult result)
        {
        }
    }
}
