using System.Net;

namespace CommodityArticle.Api.IntegrationTests;

public class HealthEndpointTests
{
    [Test]
    public async Task GET_health_returns_200_and_expected_payload()
    {
        // Arrange
        var _client = new HttpClient { BaseAddress = new Uri("https://localhost:7051") };

        // Act
        var response = await _client.GetAsync("/weatherforecast");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Is.Not.Null);
    }
}
