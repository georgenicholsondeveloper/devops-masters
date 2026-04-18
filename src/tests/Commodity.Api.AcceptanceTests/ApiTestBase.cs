using System;
using RestSharp;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace Commodity.Api.AcceptanceTests;

public abstract class ApiTestBase
{
    protected RestClient Client = null!;
    protected readonly IConfiguration Configuration;

    protected ApiTestBase()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.acceptance.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
    }

    protected string BaseUrl =>
        Configuration["BaseUrl"] ?? "http://localhost:8080";

    protected string HostName =>
        Configuration["ApiHostName"] ?? "localhost";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = new RestClientOptions(BaseUrl)
        {
            ThrowOnAnyError = false
        };

        Client = new RestClient(options);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Client?.Dispose();
    }
}