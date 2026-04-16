using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace Commodity.Api.AcceptanceTests;

public abstract class ApiTestBase
{
    protected IPlaywright Playwright = null!;
    protected IAPIRequestContext Request = null!;

    
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

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Request = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = BaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["Accept"] = "application/json"
            }
        });
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (Request is not null)
            await Request.DisposeAsync();

        Playwright?.Dispose();
    }
}