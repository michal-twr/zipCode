using Core;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Infrastructure;

public static class Extensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IZipApiClient, ZippopotamClient>()
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