using Core;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;

namespace ZipCode.Tests.Integration;


public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public WireMockServer ZippopotamServer = default!;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ZippopotamServer = WireMockServer.Start();
        
        var config = new Dictionary<string, string?>
        {
            { "Zippopotam:Url", ZippopotamServer.Urls[0] },
        };
        
        builder.ConfigureAppConfiguration(x =>
            x.AddInMemoryCollection(config)
        );
        
        builder.ConfigureServices((context, services) =>
        {
            services.AddInfrastructure(context.Configuration);
        });
        
        //builder.ConfigureServices(services =>
        //{
        //    services.AddSingleton<IZipApiClient, FakeIZipApiClient>();
        //});
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ZippopotamServer.Dispose();
        }

        base.Dispose(disposing);
    }
}