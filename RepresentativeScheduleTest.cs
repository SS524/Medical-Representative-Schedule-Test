using MedicalRepresentativeScheduleMicroservice.Controllers;
using MedicalRepresentativeScheduleMicroservice.Models;
using MedicalRepresentativeScheduleMicroservice.Provider;
using MedicalRepresentativeScheduleMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalRepresentativeTest
{
    public class RepresentativeScheduleTest
    {
        List<MedicineStock> medicinestock;
        [SetUp]
        public void Setup()
        {

            medicinestock = new List<MedicineStock>
            {
            new MedicineStock{ Name= "Aciloc",ChemicalComposition="Domperidone,Omeprazole",TargetAilment="General",DateOfExpiry=new DateTime(2022,05,25),NumberOfTabletsInStock=1000},
            new MedicineStock{ Name= "Demerol",ChemicalComposition="Meperidine Hydrochloride,USP",TargetAilment="Orthopaedics",DateOfExpiry=new DateTime(2021,03,05),NumberOfTabletsInStock=2500},
            new MedicineStock{ Name= "Becosules",ChemicalComposition="Pyridoxine Hydrochloride,Thiamine,Nicotinamide,Folate",TargetAilment="General",DateOfExpiry=new DateTime(2023,12,28),NumberOfTabletsInStock=1500},
            new MedicineStock{ Name= "Cytotec",ChemicalComposition="Misoprostol,Prostaglandin E1 Analog",TargetAilment="Gynaecology",DateOfExpiry=new DateTime(2022,10,10),NumberOfTabletsInStock=1800},
            new MedicineStock{ Name= "Volini 50mg",ChemicalComposition="Diclofenac",TargetAilment="Orthopaedics",DateOfExpiry=new DateTime(2025,07,13),NumberOfTabletsInStock=600}
            };

        }

        [Test]
        public void GetByDatePassTest()
        {
            var mock = new Mock<IRepScheduleRepository>();
            mock.Setup(x => x.Get("token")).Returns(medicinestock);
            RepScheduleProvider prov = new RepScheduleProvider(mock.Object);
            DateTime date = new DateTime(2020, 05, 25);
            IEnumerable<RepSchedule> lstest = prov.GetByDate( date, "token");                    
            Assert.AreEqual(lstest.Count(),5);

        }
        [Test]
        public void GetByDateFailTest()
        {
            var mock = new Mock<IRepScheduleRepository>();
            mock.Setup(x => x.Get("token")).Returns(medicinestock);
            RepScheduleProvider prov = new RepScheduleProvider(mock.Object);
            DateTime date = new DateTime(2020, 05, 24);
            IEnumerable<RepSchedule> lstest = prov.GetByDate(date, "token");
            Assert.AreEqual(lstest.Count(), 0);
        }
        [Test]
        public void GetByDatePassTestController()
        {
            var mock = new Mock<IRepScheduleRepository>();
            mock.Setup(x => x.Get("token")).Returns(medicinestock);
            RepScheduleProvider prov = new RepScheduleProvider(mock.Object);
            DateTime date = new DateTime(2020, 05, 25);
            RepScheduleController repcon = new RepScheduleController(prov);
            var data = repcon.Get(date);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);


        }
        [Test]
        public void GetByDateFailTestController()
        {
            var mock = new Mock<IRepScheduleRepository>();
            mock.Setup(x => x.Get("token")).Returns(medicinestock);
            RepScheduleProvider prov = new RepScheduleProvider(mock.Object);
            DateTime date = new DateTime(2020, 05, 24);
            RepScheduleController repcon = new RepScheduleController(prov);
            var data = repcon.Get(date);
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);


        }

    }
}