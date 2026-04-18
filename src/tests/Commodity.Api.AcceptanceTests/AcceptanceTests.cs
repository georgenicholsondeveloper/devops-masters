using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
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

        var createRequest = new RestRequest("/api/commodity", Method.Post);
        
        // Add Host header for nginx ingress routing
        createRequest.AddHeader("Host", HostName);
        createRequest.AddHeader("Accept", "application/json");
        createRequest.AddJsonBody(payload);

        // Act
        var createResponse = await Client.ExecuteAsync<CommodityModel>(createRequest);

        // Assert
        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(createResponse.Data, Is.Not.Null);
        
        var createdId = createResponse.Data!.Id;
        Assert.That(createdId, Is.Not.Null);

        // Arrange GET request
        var getRequest = new RestRequest($"/api/commodity/{createdId}", Method.Get);
        getRequest.AddHeader("Host", HostName);
        getRequest.AddHeader("Accept", "application/json");

        // Act
        var getResponse = await Client.ExecuteAsync<CommodityModel>(getRequest);

        // Assert
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(getResponse.Data, Is.Not.Null);
        Assert.That(getResponse.Data!.Name, Is.EqualTo("Gold"));
    }
}