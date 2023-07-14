namespace Core;

public interface IZipApiClient
{
    public Task<string> GetZipName(string zipCode);
}