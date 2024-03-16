using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CountriesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost]
        public IActionResult GetSecondLargestInteger(RequestObj requestObj)
        {
            var result = _collectionService.ReturnSecondLargestInteger(requestObj);
            return Ok(result);
        }
    }
}
