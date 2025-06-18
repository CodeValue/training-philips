using System;
using System.Collections.Generic;
using NUnit.Framework;
using OrganLens.Legacy.Core;

namespace OrganLens.Legacy.Core.Tests
{
    [TestFixture]
    public class StudyManagerTests
    {
        [SetUp]
        public void SetUp()
        {
            var kidney = new StudyRecord { StudyId = "SYN-0002", Modality = "CT", StudyDate = new DateTime(2026, 2, 3), SliceThicknessMm = 1.25 };
            kidney.Patient.PatientId = "P-1002";
            kidney.Patient.PatientName = "Test Patient 02";
            kidney.Organs.Add("Kidney");
            kidney.Metrics["LengthMm"] = 110;
            kidney.Metrics["WidthMm"] = 50;
            kidney.Metrics["DepthMm"] = 45;

            var heart = new StudyRecord { StudyId = "SYN-0003", Modality = "MR", StudyDate = new DateTime(2026, 2, 20), SliceThicknessMm = 8.0 };
            heart.Patient.PatientId = "P-1003";
            heart.Patient.PatientName = "Test Patient 03";
            heart.Organs.Add("Heart");
            heart.Metrics["EdvMl"] = 120;
            heart.Metrics["EsvMl"] = 48;

            StudyManager.Instance.SetStudies(new List<StudyRecord> { kidney, heart });
        }

        [Test]
        public void EjectionFraction_IsReturnedInPercent()
        {
            Assert.AreEqual(60.0, StudyManager.Instance.ComputeEjectionFraction(120, 48), 0.01);
        }

        [Test]
        public void SpleenIndex_UsesCentimetres()
        {
            Assert.AreEqual(378.0, StudyManager.Instance.ComputeSpleenIndex(120, 70, 45), 0.01);
        }

        [Test]
        public void Search_FindsByStudyId()
        {
            var result = StudyManager.Instance.Search("0003");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("SYN-0003", result[0].StudyId);
        }

        [Test]
        public void KidneyVolume_MatchesWebAnalysis()
        {
            // Web (OrganLens.Analysis) returns 129.6 mL for this kidney.
            Assert.AreEqual(129.6, StudyManager.Instance.ComputeKidneyVolume(110, 50, 45), 0.01);
        }
    }
}
