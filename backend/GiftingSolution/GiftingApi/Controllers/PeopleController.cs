using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftingApi.Controllers
{
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ICatalogPeople _personCatalog;

        public PeopleController(ICatalogPeople catalogPeople)
        {
            _personCatalog = catalogPeople;
        }

        [HttpGet("/people")]
        public async Task<ActionResult<PersonResponse>> GetAllPeople()
        {
            PersonResponse response = await _personCatalog.GetPeopleAsync();
            return Ok(response);
        }

        [HttpPost("/people")]
        //[Authorize]
        public async Task<ActionResult<PersonItemResponse>> AddPerson([FromBody] PersonCreateRequest request)
        {
            //validate the request
            //if(!ModelState.IsValid) return BadRequest(ModelState);


            PersonItemResponse response = await _personCatalog.AddPersonAsync(request);
            return StatusCode(201, response);
            //if it is valid, add it to DB, return 200

            //if it's not valid, send 400
        }

        [HttpGet("/people/{id:int}")]
        public async Task<ActionResult<PersonItemResponse>> GetPersonById(int id)
        {
            PersonItemResponse? response = await _personCatalog.GetPersonByIdAsync(id);
            if (response is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
