# Graph Report - organlens  (2026-11-05)

## Corpus Check
- 75 files · ~8,611 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 6 file(s) not represented in the graph (top: (none) 1, .props 1, .xml 1)

## Summary
- 459 nodes · 846 edges · 19 communities (16 shown, 3 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 50 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `860ae656`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- AnalysisResult
- StudyEntity
- StudyRecord
- system
- OrganLens.sln
- StudySummaryDto
- MainForm
- OrganLens.Analysis
- Organ
- AnalysisResultDto
- OrganLens.Analysis.Calculators
- Fact
- package.json
- Endpoints
- http
- app.js
- StudyManagerTests
- OrganLens
- e2e/README.md

## God Nodes (most connected - your core abstractions)
1. `StudyRecord` - 28 edges
2. `StudyEntity` - 26 edges
3. `AnalysisResult` - 19 edges
4. `ImageSeries` - 19 edges
5. `Organ` - 19 edges
6. `MainForm` - 19 edges
7. `OrganLens.Analysis` - 18 edges
8. `MeasurementDto` - 18 edges
9. `OrganLens.Api.Models` - 17 edges
10. `StudyManager` - 17 edges

## Surprising Connections (you probably didn't know these)
- `Domain glossary` --references--> `ConfidenceModel`  [INFERRED]
  docs/domain-glossary.md → src/OrganLens.Analysis/ConfidenceModel.cs
- `AnalysisResult` --references--> `Organ`  [EXTRACTED]
  src/OrganLens.Analysis/AnalysisResult.cs → src/OrganLens.Analysis/Organ.cs
- `StudyEntity` --references--> `AnalysisResult`  [EXTRACTED]
  src/OrganLens.Api/Data/StudyEntity.cs → src/OrganLens.Analysis/AnalysisResult.cs
- `AnalysisService` --references--> `AnalyzerRegistry`  [EXTRACTED]
  src/OrganLens.Api/Services/AnalysisService.cs → src/OrganLens.Analysis/AnalyzerRegistry.cs
- `LungAnalyzer` --implements--> `IOrganAnalyzer`  [EXTRACTED]
  src/OrganLens.Analysis/Analyzers/LungAnalyzer.cs → src/OrganLens.Analysis/IOrganAnalyzer.cs

## Import Cycles
- None detected.

## Communities (19 total, 3 thin omitted)

### Community 0 - "AnalysisResult"
Cohesion: 0.06
Nodes (37): Domain glossary, Manual measurement kinds, AnalysisResult, AnalyzedAtUtc, Findings, Measurements, Organ, LungAnalyzer (+29 more)

### Community 1 - "StudyEntity"
Cohesion: 0.07
Nodes (29): MeasurementsController, InMemoryStudyStore, IStudyStore, SeedData, StudyEntity, Analyses, Id, Measurements (+21 more)

### Community 2 - "StudyRecord"
Cohesion: 0.10
Nodes (16): ReportBuilder, StudyManager, Instance, StudyRecord, Metrics, Modality, Organs, Patient (+8 more)

### Community 3 - "system"
Cohesion: 0.09
Nodes (7): OrganLens.Legacy.Core, OrganLens.Legacy.Data, OrganLens.Legacy.Core.Tests, OrganLens.Legacy.Core.Utils, OrganLens.Legacy.WinForms, PatientInfo, MathHelper

### Community 4 - "OrganLens.sln"
Cohesion: 0.07
Nodes (25): netstandard2.0, Microsoft.AspNetCore.Mvc.Testing (8.0.10), NUnit (3.14.0), NUnit3TestAdapter (4.6.0), xunit (2.9.2), xunit.runner.visualstudio (2.8.2), Microsoft.NET.Sdk.Web, Microsoft.NET.Sdk (+17 more)

### Community 5 - "StudySummaryDto"
Cohesion: 0.09
Nodes (19): AnalysisController, HealthController, StudiesController, SeriesDto, FrameCount, Organ, SeriesId, SliceThicknessMm (+11 more)

### Community 7 - "OrganLens.Analysis"
Cohesion: 0.17
Nodes (6): OrganLens.Api.Models, OrganLens.Api.Data, OrganLens.Api.Controllers, OrganLens.Analysis, OrganLens.Api.Services, OrganLens.Api.Tests.Integration

### Community 8 - "Organ"
Cohesion: 0.12
Nodes (16): OrganLens.Analysis.Analyzers, AnalyzerRegistry, AnalyzerHelpers, HeartAnalyzer, Organ, KidneyAnalyzer, Organ, LiverAnalyzer (+8 more)

### Community 9 - "AnalysisResultDto"
Cohesion: 0.10
Nodes (18): AnalysisRequest, Organ, AnalysisResultDto, AnalyzedAtUtc, Findings, Measurements, Organ, StudyId (+10 more)

### Community 10 - "OrganLens.Analysis.Calculators"
Cohesion: 0.13
Nodes (7): OrganLens.Analysis.Calculators, OrganLens.Api.Tests.Unit, EjectionFractionCalculator, KidneyVolumeCalculator, ConfidenceModelTests, EjectionFractionCalculatorTests, KidneyVolumeCalculatorTests

### Community 11 - "Fact"
Cohesion: 0.22
Nodes (4): Program, AnalysisEndpointTests, ApiFactory, StudiesEndpointTests

### Community 12 - "package.json"
Cohesion: 0.18
Nodes (9): @playwright/test, devDependencies, @playwright/test@^1.48.0, name, private, scripts, report, test (+1 more)

### Community 13 - "Endpoints"
Cohesion: 0.18
Nodes (10): Endpoints, General rules, `GET /api/health`, `GET /api/studies`, `GET /api/studies/{id}`, `GET /api/studies/{id}/measurements`, OrganLens Web API — contract, `POST /api/studies/{id}/analysis` (+2 more)

### Community 14 - "http"
Cohesion: 0.20
Nodes (9): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, profiles, http (+1 more)

### Community 15 - "app.js"
Cohesion: 0.47
Nodes (8): analyze(), escapeHtml(), formatValue(), loadStudies(), openStudy(), renderMeasurements(), showError(), submitMeasurement()

### Community 17 - "OrganLens"
Cohesion: 0.33
Nodes (5): OrganLens, Run the desktop app (Windows), Run the web app, Tests, What's in here

## Knowledge Gaps
- **113 isolated node(s):** `Organ`, `Measurements`, `Findings`, `AnalyzedAtUtc`, `Organ` (+108 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 176 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **3 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `AnalysisResult` connect `AnalysisResult` to `StudyEntity`, `StudyRecord`, `system`, `Organ`, `AnalysisResultDto`?**
  _High betweenness centrality (0.087) - this node is a cross-community bridge._
- **What connects `Organ`, `Measurements`, `Findings` to the rest of the system?**
  _113 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `AnalysisResult` be split into smaller, more focused modules?**
  _Cohesion score 0.05587808417997097 - nodes in this community are weakly interconnected._
- **Why does `StudyEntity` connect `StudyEntity` to `AnalysisResult`, `StudyRecord`, `StudySummaryDto`, `OrganLens.Analysis`, `AnalysisResultDto`?**
  _High betweenness centrality (0.081) - this node is a cross-community bridge._
- **Should `StudyEntity` be split into smaller, more focused modules?**
  _Cohesion score 0.0663265306122449 - nodes in this community are weakly interconnected._
- **Why does `ImageSeries` connect `AnalysisResult` to `Organ`, `StudyEntity`, `StudyRecord`, `AnalysisResultDto`?**
  _High betweenness centrality (0.079) - this node is a cross-community bridge._
- **Should `StudyRecord` be split into smaller, more focused modules?**
  _Cohesion score 0.09595959595959595 - nodes in this community are weakly interconnected._