# OrganLens Web API — contract

This document is the **source of truth** for the web API. If code and this document disagree, the code is wrong (or this document must be updated in the same PR).

Base URL (local): `http://localhost:5080`
JSON uses camelCase. Dates: `yyyy-MM-dd`. Timestamps: ISO-8601 UTC.

## General rules

- **No PHI in responses or logs.** Patients are identified only by `patientRef` (pseudonymous). Patient names must never leave the server and must never be written to logs.
- **Errors** use RFC 7807 Problem Details (`application/problem+json`):
  ```json
  { "type": "...", "title": "Study not found", "status": 404, "detail": "No study with id 'SYN-9999'." }
  ```
- **Validation errors** return `400` with an `errors` object: field name → list of messages.
  ```json
  {
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": { "sliceThicknessMm": ["Must be greater than 0 and at most 10 mm."] }
  }
  ```
- Bad input must **never** produce a `500`.

## Units

| Measurement | Unit | Notes |
|---|---|---|
| Kidney `Volume` | `mL` | 1 decimal |
| Heart `EjectionFraction` | `%` | **percent, 0–100**, 1 decimal (e.g. `60.0`, not `0.6`) |
| Liver `MeanDensity` | `HU` | Hounsfield units, can be negative |
| Lung `NoduleCount` | `count` | |
| Finding `confidence` | — | 0–1 (e.g. `0.68`) |

## Endpoints

### `GET /api/health`
`200` → `{ "status": "ok" }`

### `GET /api/studies`
`200` → array of study summaries:
```json
[{ "id": "SYN-0002", "patientRef": "P-1002", "modality": "CT", "studyDate": "2026-02-03", "organs": ["Kidney"], "status": "New" }]
```
`status` is `New` or `Analyzed`.

### `GET /api/studies/{id}`
`200` → summary fields + `series` (`seriesId`, `organ`, `frameCount`, `sliceThicknessMm`) + `measurements` (manual measurements).
`404` → Problem Details.

### `POST /api/studies/{id}/analysis`
Body: `{ "organ": "Kidney" }` (case-insensitive). Supported: `Liver`, `Kidney`, `Lung`, `Heart`.
`200` →
```json
{
  "studyId": "SYN-0002",
  "organ": "Kidney",
  "measurements": [{ "name": "Volume", "value": 129.6, "unit": "mL" }],
  "findings": [],
  "analyzedAtUtc": "2026-05-01T10:00:00Z"
}
```
Finding: `{ "code": "KID-ENLARGED", "description": "...", "confidence": 0.68, "severity": "Medium" }`
`400` → unsupported organ, or the study has no series for that organ.
`404` → unknown study.

### `GET /api/studies/{id}/measurements`
`200` → array of manual measurements. `404` → unknown study.

### `POST /api/studies/{id}/measurements`
A measurement entered manually by a radiologist.
Body:
```json
{ "organ": "Kidney", "kind": "Length", "value": 110, "unit": "mm", "sliceThicknessMm": 1.25 }
```
`201` →
```json
{ "id": 1, "organ": "Kidney", "kind": "Length", "value": 110, "unit": "mm", "sliceThicknessMm": 1.25, "slicesCovered": 88, "createdAtUtc": "..." }
```
`slicesCovered` = how many slices the measured value spans (`value / sliceThicknessMm`, rounded down).
`404` → unknown study.
Validation rules: _TBD_ (see measurement kinds in `domain-glossary.md`).
