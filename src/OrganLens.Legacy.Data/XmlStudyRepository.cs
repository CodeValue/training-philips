using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using OrganLens.Legacy.Core;

namespace OrganLens.Legacy.Data
{
    /// <summary>
    /// Default repository. Reads data/legacy-studies.xml.
    /// </summary>
    public class XmlStudyRepository : IStudyRepository
    {
        private readonly string _path;

        public XmlStudyRepository(string path)
        {
            _path = path;
        }

        public List<StudyRecord> LoadAll()
        {
            var doc = XDocument.Load(_path);
            var list = new List<StudyRecord>();

            foreach (var el in doc.Root.Elements("Study"))
            {
                var s = new StudyRecord();
                s.StudyId = (string)el.Attribute("Id");
                s.Patient.PatientId = (string)el.Attribute("PatientId");
                s.Patient.PatientName = (string)el.Attribute("PatientName");
                s.Patient.BirthYear = int.Parse((string)el.Attribute("BirthYear"), CultureInfo.InvariantCulture);
                s.StudyDate = DateTime.ParseExact((string)el.Attribute("Date"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                s.Modality = (string)el.Attribute("Modality");
                s.SliceThicknessMm = double.Parse((string)el.Attribute("SliceThicknessMm"), CultureInfo.InvariantCulture);

                foreach (var organ in el.Elements("Organ"))
                {
                    s.Organs.Add((string)organ.Attribute("Name"));
                }

                foreach (var metric in el.Elements("Metric"))
                {
                    s.Metrics[(string)metric.Attribute("Name")] = double.Parse((string)metric.Attribute("Value"), CultureInfo.InvariantCulture);
                }

                list.Add(s);
            }

            return list;
        }

        public List<StudyRecord> SearchByPatientName(string name)
        {
            return LoadAll()
                .Where(s => s.Patient.PatientName.IndexOf(name ?? string.Empty, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
    }
}
