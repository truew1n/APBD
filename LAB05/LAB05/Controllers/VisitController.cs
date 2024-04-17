using Microsoft.AspNetCore.Mvc;
using LAB05.Classes;
using LAB05.Services;

namespace LAB05.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitController : ControllerBase
    {
        private IMockDb _mockDb;
        public VisitController(IMockDb mockDb)
        {
            _mockDb = mockDb;
        }

        [HttpPost("", Name = "AddVisit")]
        public IActionResult AddVisit([FromBody] Visit visit)
        {
            Visit? returnedVisit = _mockDb.AddVisit(visit);
            if (returnedVisit == null)
            {
                return BadRequest("Invalid Visit data provided by client.");
            }
            if (returnedVisit.Animal == null)
            {
                return BadRequest("Invalid Animal data provided by client.");
            }

            return Created($"visits/{visit}", visit);
        }

        [HttpGet("{animalId}", Name = "GetAllVisitsByAnimalId")]
        public IActionResult GetAllVisitsByAnimalId(Int32 animalId)
        {
            return Ok(_mockDb.GetAllVisitsByAnimalId(animalId));
        }
    }
}
