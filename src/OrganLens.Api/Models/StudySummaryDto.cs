namespace OrganLens.Api.Models;

public class StudySummaryDto
{
    public string Id { get; set; }

    /// <summary>Pseudonymous patient reference. Never the patient's name.</summary>
    public string PatientRef { get; set; }

    /// <summary>Patient display name, for convenience in the UI.</summary>
    public string PatientName { get; set; }

    public string Modality { get; set; }

    /// <summary>yyyy-MM-dd</summary>
    public string StudyDate { get; set; }

    public string[] Organs { get; set; }

    /// <summary>"New" or "Analyzed".</summary>
    public string Status { get; set; }
}
