using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Infrastructure;

public static class Extensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration.GetValue<string>("Zippopotam:Url");
        services.AddHttpClient<IZipApiClient, ZippopotamClient>(x => x.BaseAddress = new Uri(url))
            .SetHandlerLifetime((TimeSpan.FromMinutes(2)))
            .AddPolicyHandler(GetRetryPolicy());
            
        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retry => TimeSpan.FromMilliseconds(10));
    }
    
}