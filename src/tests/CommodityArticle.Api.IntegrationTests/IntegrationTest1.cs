using System.Net;
using System.Net.Http.Json;

namespace CommodityArticle.Api.IntegrationTests;

public class HealthEndpointTests
{
    [Test]
    public async Task GET_health_returns_200_and_expected_payload()
    {
        // Arrange
        var _client = new HttpClient { BaseAddress = new Uri("https://localhost:8080") };

        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        Assert.Equals(HttpStatusCode.OK, response.StatusCode);

        // Optional: if /health returns JSON like { "status": "ok" }
        var payload = await response.Content.ReadFromJsonAsync<HealthResponse>();
        Assert.NotNull(payload);
        Assert.Equals("ok", payload!.status);
    }

    private sealed record HealthResponse(string status);
}
