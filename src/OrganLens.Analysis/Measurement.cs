namespace OrganLens.Analysis
{
    public class Measurement
    {
        public Measurement()
        {
        }

        public Measurement(string name, double value, string unit)
        {
            Name = name;
            Value = value;
            Unit = unit;
        }

        public string Name { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
    }
}
