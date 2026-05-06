# Domain glossary

| Term | Meaning |
|---|---|
| **Study** | One imaging exam of one patient on one date (e.g. a CT of the abdomen). Id like `SYN-0002`. |
| **Series** | A set of images inside a study, here one per organ. |
| **Frame / slice** | One 2D image in a series. |
| **Modality** | Imaging technique: `CT` (computed tomography) or `MR` (magnetic resonance). |
| **Slice thickness** | How thick each slice is, in mm. Thinner = more detail. Typical: 0.5–5 mm, rarely above 10 mm. |
| **HU (Hounsfield units)** | CT density scale. Air ≈ −1000, fat ≈ −100, water = 0, healthy liver ≈ 50–65, bone up to ≈ +3000. A fatty liver can be below 0. |
| **Ejection fraction (EF)** | % of blood the heart pumps out per beat: `(EDV − ESV) / EDV × 100`. Normal ≈ 55–70%. Below 50% = reduced. |
| **EDV / ESV** | End-diastolic / end-systolic volume of the left ventricle, in mL. |
| **Ellipsoid volume** | Organ volume estimate: `length × width × depth × π/6`. Used for kidneys. |
| **Spleen index** | `L × W × T` in cm. Desktop app only. |
| **Nodule** | Small round spot in the lung. ≥ 6 mm is reported. |
| **Finding** | Something the analysis flags (e.g. `KID-ENLARGED`). Has a severity and a confidence (0–1). |
| **Confidence** | How much to trust a finding. Today it is derived from slice thickness only (see `ConfidenceModel`). |
| **PatientRef** | Pseudonymous patient id (e.g. `P-1002`). Safe to show. The patient's name is PHI. |
| **PHI** | Protected Health Information. Never in logs, prompts, tickets or API responses. |

## Manual measurement kinds

Radiologists can add measurements by hand (`POST /api/studies/{id}/measurements`).

| Organ | Kind | Unit | Plausible range |
|---|---|---|---|
| Kidney | `Length`, `Width`, `Depth` | `mm` | 1–250 |
| Heart | `EDV`, `ESV` | `mL` | 1–500 |
| Liver | `MeanDensity` | `HU` | −1000 to 3000 (can be negative!) |
| Lung | `NoduleDiameter` | `mm` | 1–100 |
