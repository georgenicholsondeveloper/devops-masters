using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Commodity.Api.SmokeTests;

[TestFixture]
public class SmokeTests : ApiTestBase
{
    [Test]
    public async Task GetDefaultEndpoint_Returns_SuccessMessage()
    {
        var getResponse = await Request.GetAsync("/");
        var healthMessage = await getResponse.JsonAsync<string>();

        Assert.That(getResponse.Status, Is.EqualTo((int)HttpStatusCode.OK));
        Assert.That(healthMessage, Is.EqualTo("The Commodity API is up and running."));
    }
}