using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Service.Contracts;

namespace CountriesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _service;

        public CountriesController(ICountryService service) => _service = service;


        [HttpGet]
        [EnableRateLimiting("CountriesPolicy")]
        public async Task<IActionResult> GetAllCountries(CancellationToken cancellationToken)
        {
            var countries = await _service.GetAllCountriesAsync(trackChanges: false, cancellationToken);

            return Ok(countries);
        }
    }
}
