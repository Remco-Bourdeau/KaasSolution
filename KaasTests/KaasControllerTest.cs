using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaasService.Controllers;
using KaasService.Models;
using KaasService.Repositories;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KaasTests
{
    [TestClass]
    public class KaasControllerTest
    {
        private KaasController controller;
        private Mock<IKaasRepository> mock;
        private Kaas kaas7, kaas9;

        [TestInitialize]
        public void Init()
        {
            mock = new Mock<IKaasRepository>();
            var repository = mock.Object;
            controller = new KaasController(repository);
            kaas7 = new Kaas { Id = 7, Naam = "7", Soort="7", Smaak ="7"};
            kaas9 = new Kaas { Id = 9, Naam = "9", Soort = "9", Smaak = "9" };
        }
        //TESTS GETALL/FINDBYID/FINDBYSMAAK/PUT
        [TestMethod]
        public void GetAllGeeftAlleKazen()
        {
            var alleKazen = new List<Kaas>() { kaas7, kaas9 };
            mock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(alleKazen));
            var response = (OkObjectResult)controller.GetAll().Result;
            var kazen = (List<Kaas>)response.Value;
            Assert.AreEqual(2, kazen.Count);
            Assert.AreEqual(7, kazen[0].Id);
            mock.Verify(repo => repo.GetAllAsync());
        }
        [TestMethod]
        public void FindByIdOnbestaandeKaas()
        {
            Assert.IsInstanceOfType(controller.FindById(7).Result, typeof(NotFoundResult));
            mock.Verify(repo => repo.FindByIdAsync(7));
        }
        [TestMethod]
        public void FindByIdBestaandeKaas()
        {
            mock.Setup(repo => repo.FindByIdAsync(7)).Returns(Task.FromResult(kaas7));
            var response = (OkObjectResult)controller.FindById(7).Result;
            var kaas = (Kaas)response.Value;
            Assert.AreEqual(7, kaas.Id);
            mock.Verify(repo => repo.FindByIdAsync(7));
        }
        [TestMethod]
        public void FindBySmaakGeeftJuisteKazen()
        {
            var kazenMetSmaak7 = new List<Kaas>() { kaas7};
            mock.Setup(repo => repo.FindBySmaakAsync("7")).Returns(Task.FromResult(kazenMetSmaak7));
            var response = (OkObjectResult)controller.FindBySmaak("7").Result;
            var kazen = (List<Kaas>)response.Value;
            Assert.AreEqual(1, kazenMetSmaak7.Count);
            Assert.AreEqual("7", kazenMetSmaak7[0].Smaak);
            mock.Verify(repo => repo.FindBySmaakAsync("7"));
        }
        [TestMethod]
        public void PutNotFoundOnbestaandeKaas()
        {
            mock.Setup(repo => repo.UpdateAsync(kaas7)).Throws(new DbUpdateConcurrencyException());
            Assert.IsInstanceOfType(controller.Put(7, kaas7).Result, typeof(NotFoundResult));
            mock.Verify(repo => repo.UpdateAsync(kaas7));
        }
        [TestMethod]
        public void PutVerandertKaas()
        {
            Assert.IsInstanceOfType(controller.Put(7, kaas7).Result, typeof(OkResult));
            mock.Verify(repo => repo.UpdateAsync(kaas7));
        }
    }
}
