using FluentAssertions;
using Infrastructure;
using WireMock.RequestBuilders;
using WireMock.Server;
using static WireMock.ResponseBuilders.Response;

namespace ZipCode.Tests.Integration;

public class UnitTest1
{
    [Fact]
    public async Task Test2()
    {
        var server = WireMockServer.Start();
        server.Given(
            Request.Create()
                .WithPath("/us/90210")
        ).RespondWith(Create().WithBody("""
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
        var zip = new ZippopotamClient(server.CreateClient());

        var response = await zip.GetZipName("90210");
        response.Should().Be("Beverly Hills - CA US");
    }
    

}