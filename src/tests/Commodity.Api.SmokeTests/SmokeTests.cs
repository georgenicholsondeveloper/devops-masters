using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;

namespace Commodity.Api.SmokeTests;

[TestFixture]
public class SmokeTests : ApiTestBase
{
    [Test]
    public async Task GetDefaultEndpoint_Returns_SuccessMessage()
    {
        // Arrange
        var request = new RestRequest("/", Method.Get);
        
        // Add Host header for nginx ingress routing
        request.AddHeader("Host", HostName);
        request.AddHeader("Accept", "application/json");

        // Act
        var response = await Client.ExecuteAsync<string>(request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.EqualTo($"The Commodity {Configuration["Environment"]} API is up and running."));
    }
}