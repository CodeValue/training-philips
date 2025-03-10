using System.Collections.Generic;
using OrganLens.Legacy.Core;

namespace OrganLens.Legacy.Data
{
    public interface IStudyRepository
    {
        List<StudyRecord> LoadAll();

        List<StudyRecord> SearchByPatientName(string name);
    }
}
