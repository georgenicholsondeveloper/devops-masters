using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NUnit.Framework;
using Commodity.Api.DTOs;
using Commodity.Api.Models;

namespace Commodity.Api.AcceptanceTests;

[TestFixture]
public class CommodityApiAcceptanceTests : ApiTestBase
{
    [Test]
    public async Task CreateCommodity_then_get_it_back()
    {
        // Arrange
        var payload = new CreateCommodityDto()
        {
            Name = "Gold",
            Price = 1.0M,
            Category = "Metal"
        };

        // Act
        var createHttpResponse = await Request.PostAsync("/api/commodity", new()
        {
            DataObject = payload
        });

        // Assert
        Assert.That(createHttpResponse.Status, Is.EqualTo((int)HttpStatusCode.Created));

        var createdJsonResponse = await createHttpResponse.TextAsync();

        var createResponse = JsonSerializer.Deserialize<CommodityModel>(createdJsonResponse);

        var createdId = createResponse!.Id;

        Assert.NotNull(createdId);

        // Act
        var getResponse = await Request.GetAsync($"/api/commodity/{createdId}");

        // Assert
        Assert.That(getResponse.Status, Is.EqualTo((int)HttpStatusCode.OK));

        var retrievedJsonResponse = await getResponse.TextAsync();

        var retrieveResponse = JsonSerializer.Deserialize<CommodityModel>(retrievedJsonResponse);

        Assert.That(retrieveResponse!.Name, Is.EqualTo("Gold"));
    }
}