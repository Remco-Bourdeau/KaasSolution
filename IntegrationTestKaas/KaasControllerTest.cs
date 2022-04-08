using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using KaasService.Repositories;
using Moq;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using KaasService.Models;

namespace IntegrationTestKaas
{
    [TestClass]
    public class KaasControllerTest
    {
        private HttpClient client;
        private Mock<IKaasRepository> mock;
        private Kaas kaas1;
        [TestInitialize]
        public void init()
        {
            mock = new Mock<IKaasRepository>();
            var repo = mock.Object;
            var factory = new WebApplicationFactory<KaasService.Startup>();
            client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(
                services => services.AddScoped<IKaasRepository>(_ => repo))).CreateClient();
            kaas1 = new Kaas { Id = 1, Naam = "1", Soort = "1", Smaak = "1" };
        }
        [TestMethod]
        public void FindByIdOnbestaand()
        {
            var response = client.GetAsync("kazen/-1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(-1));
        }
        [TestMethod]
        public void FindByIdBestaand()
        {
            mock.Setup(repo => repo.FindByIdAsync(1)).Returns(Task.FromResult(kaas1));
            var response = client.GetAsync("kazen/1").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(1));
            var body = response.Content.ReadAsStringAsync().Result;
            var doc = JsonDocument.Parse(body);
            Assert.AreEqual(1, doc.RootElement.GetProperty("id").GetInt32());
            Assert.AreEqual("1", doc.RootElement.GetProperty("naam").GetString());
        }
    }
}
