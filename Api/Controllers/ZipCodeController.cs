using Core;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ZipCodeController : ControllerBase
{
    private readonly ZipCodeService _zipCodeService;

    public ZipCodeController(ZipCodeService zipCodeService)
    {
        _zipCodeService = zipCodeService;
    }

    [HttpGet("/zipcode/{zipCode}", Name = "zipCode" )]
    public async Task<object> Getx(string zipCode)
    {
        return new { name = await _zipCodeService.processZipCode(zipCode) };
    }
    
}