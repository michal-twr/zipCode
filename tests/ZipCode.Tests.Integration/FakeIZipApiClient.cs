using Core;

namespace ZipCode.Tests.Integration;

public class FakeIZipApiClient : IZipApiClient
{
    public Task<string> GetZipName(string zipCode)
    {
        return Task.FromResult("coskolwiek");
    }
}