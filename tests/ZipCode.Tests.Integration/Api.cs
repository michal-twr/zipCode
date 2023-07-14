using Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ZipCode.Tests.Integration;

public class Api : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public Api(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Test1()
    {
        var client = _factory.CreateClient();
        
        var server = _factory.ZippopotamServer;
        server.Given(
            Request.Create()
                .WithPath("/us/90210")
        ).RespondWith(Response.Create().WithBody("""
            {
              "post code": "90210",
              "country": "United States",
              "country abbreviation": "US",
              "places": [
                {
                  "place name": "Beverly Hills",
                  "longitude": "-118.4065",
                  "state": "California",
                  "state abbreviation": "CA",
                  "latitude": "34.0901"
                }
              ]
            }
        """));

        
        var response = await client.GetAsync($"zipcode/90210");

        response.EnsureSuccessStatusCode();
        
        Assert.Equal(true, true);
        ;
    }
}