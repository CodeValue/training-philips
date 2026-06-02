using System.Text.Json.Serialization;
using OrganLens.Analysis;
using OrganLens.Api.Data;
using OrganLens.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // cleaner payloads
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddMemoryCache();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IStudyStore>(_ => new InMemoryStudyStore(SeedData.Create()));
builder.Services.AddSingleton(AnalyzerRegistry.CreateDefault());
builder.Services.AddScoped<StudyService>();
builder.Services.AddScoped<AnalysisService>();
builder.Services.AddScoped<MeasurementService>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();

// Needed so integration tests can use WebApplicationFactory<Program>.
public partial class Program { }
