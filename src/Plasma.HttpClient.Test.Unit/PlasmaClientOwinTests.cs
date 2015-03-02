using System.IO;
using System.Net;
using NUnit.Framework;
using Plasma.Core;
using Plasma.Sample.Web.Mvc.OwinHosted;

namespace Plasma.HttpClient.Test.Unit
{
    [TestFixture]
    public class PlasmaClientOwinTests
    {
        private System.Net.Http.HttpClient _client;

        [SetUp]
        public void SetUp()
        {
	        _client = PlasmaClient.For(Path.GetFullPath(@".\..\..\..\web\Plasma.Sample.Web.Mvc.OwinHosted"),
		        configure: app =>
		        {
			        app.UseIntegratedPipeline = true;
		        });
        }

        [Test]
        public void CanCreateHttpClientForType()
        {
            var client = PlasmaClient.For(new AspNetApplication(typeof(Startup)));

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void CanCreateHttpClientForPath()
        {
			var client = PlasmaClient.For(Path.GetFullPath(@".\..\..\..\web\Plasma.Sample.Web.Mvc.OwinHosted"));

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void CanCreateHttpClientForGenericType()
        {
            var client = PlasmaClient.For<Startup>();

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public async void GetCallGet()
        {
            var response = await _client.GetAsync("/");

	        var body = response.Content.ReadAsStringAsync().Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}