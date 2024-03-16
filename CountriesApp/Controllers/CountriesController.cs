using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CountriesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CountriesController(IServiceManager service) => _service = service;


        [HttpGet]
        public async Task<IActionResult> GetAllCountries(CancellationToken cancellationToken)
        {
            var countries = await _service.CountryService.GetAllCountriesAsync(trackChanges: false, cancellationToken);

            return Ok(countries);
        }
    }
}
