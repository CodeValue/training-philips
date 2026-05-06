# OrganLens

Analyses medical images per organ (liver, kidney, lung, heart...).

> **Training repository.** All data is synthetic. No real patients, no PHI.

## What's in here

| Path | What |
|---|---|
| `src/OrganLens.Legacy.*` | The desktop app (.NET Framework 4.8, WinForms). Windows only. |
| `src/OrganLens.Analysis` | Shared analysis library (.NET Standard 2.0). |
| `src/OrganLens.Api` | Web API + web UI (.NET 8). |
| `src/*.Tests` | Unit and integration tests. |
| `tests/e2e` | Playwright end-to-end tests. |
| `docs/` | API contract and glossary. |
| `data/` | Synthetic studies for the desktop app. |

## Run the web app

```bash
dotnet run --project src/OrganLens.Api
```
Open http://localhost:5080.

## Run the desktop app (Windows)

Open `OrganLens.sln` in Visual Studio, set `OrganLens.Legacy.WinForms` as startup project, F5.

## Tests

```bash
dotnet test src/OrganLens.Api.Tests              # web: unit + integration
dotnet test src/OrganLens.Legacy.Core.Tests      # legacy (Windows)
```
E2E: see `tests/e2e/README.md`.
