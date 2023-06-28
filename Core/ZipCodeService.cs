namespace Core;

public class ZipCodeService
{
    private IZipApiClient _zipApiClient;

    public ZipCodeService(IZipApiClient zipApiClient)
    {
        _zipApiClient = zipApiClient;
    }

    public async Task<string>  processZipCode(string zipCode)
    {
        string zipName = await getZipCodeName(zipCode);

        saveZipCodeToSpredSheet(zipName);
        return zipName;
    }

    private void saveZipCodeToSpredSheet(string zipName)
    {
        //throw new NotImplementedException();
    }

    private async Task<string> getZipCodeName(string zipCode)
    {
        //TODO retry 3 times
        return await _zipApiClient.GetZipName(zipCode);
    }
    
}