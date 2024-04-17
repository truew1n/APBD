using Microsoft.AspNetCore.Mvc;
using LAB05.Classes;
using LAB05.Services;

namespace LAB05.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private IMockDb _mockDb;
        public AnimalController(IMockDb mockDb)
        {
            _mockDb = mockDb;
        }

        [HttpGet("", Name = "GetAllAnimals")]
        public IActionResult GetAllAnimals()
        {
            return Ok(_mockDb.GetAllAnimals());
        }

        [HttpGet("{id}", Name = "GetAnimalById")]
        public IActionResult GetAnimalById(Int32 id)
        {
            Animal? returnedAnimal = _mockDb.GetAnimalById(id);
            if(returnedAnimal == null)
            {
                return NotFound(new { message = "Animal not found", id = id });
            }
            return Ok(returnedAnimal);
        }

        [HttpPost("", Name = "AddAnimal")]
        public IActionResult AddAnimal(Animal animal)
        {
            _mockDb.AddAnimal(animal);
            return Created($"animals/{animal.Id}", animal);
        }

        [HttpDelete("{id}", Name = "DeleteAnimal")]
        public IActionResult DeleteAnimal(Int32 id)
        {
            Animal? returnedAnimal = _mockDb.DeleteAnimalById(id);
            if (returnedAnimal == null)
            {
                return NotFound(new { message = "Animal not found", id = id });
            }
            return Ok(returnedAnimal);
        }
        
        [HttpPut("{id}", Name = "UpdateAnimal")]
        public IActionResult UpdateAnimal(Int32 id, [FromBody] Animal updatedAnimal)
        {
            Animal? returnedAnimal = _mockDb.UpdateAnimalById(id, updatedAnimal);
            if (returnedAnimal == null)
            {
                return NotFound(new { message = "Animal not found", id = id });
            }
            return Ok(returnedAnimal);
        }
    }
}
