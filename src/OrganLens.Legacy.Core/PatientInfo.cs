namespace OrganLens.Legacy.Core
{
    // NOTE: all names in this repo are synthetic ("Test Patient NN").
    public class PatientInfo
    {
        public string PatientId;
        public string PatientName;
        public int BirthYear;

        public override string ToString()
        {
            return PatientName + " (" + PatientId + ")";
        }
    }
}
