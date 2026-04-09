using System.Net;
using Microsoft.Extensions.Configuration;

namespace Commodity.Api.IntegrationTests;

public class IntegrationTests
{
    private HttpClient _client;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        _client = new HttpClient { BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]!) };
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
    }

    [Test]
    public async Task GET_Commodity_Endpoint_Returns_Collection_Of_Commodities()
    {
        // Act
        var response = await _client.GetAsync("/api/commodity");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var content = await response.Content.ReadAsStringAsync();

        Assert.That(content, Is.Not.Null);
    }
}