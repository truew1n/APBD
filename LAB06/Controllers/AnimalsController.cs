﻿using LAB06.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace LAB06.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnimalsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAllAnimals()
        {
            var response = new List<GetAnimalsResponse>();
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand("SELECT * FROM animal", sqlConnection);
                sqlCommand.Connection.Open();

                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    response.Add(new GetAnimalsResponse(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    ));
                }
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateAnimal(CreateAnimalRequest request)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand(
                    "INSERT INTO animal (Name, Description, Category, Area) values (@1, @2, @3, @4); SELECT CAST(SCOPE_IDENTITY() as int)",
                    sqlConnection
                );
                sqlCommand.Parameters.AddWithValue("@1", request.Name);
                sqlCommand.Parameters.AddWithValue("@2", request.Description);
                sqlCommand.Parameters.AddWithValue("@3", request.Category);
                sqlCommand.Parameters.AddWithValue("@4", request.Area);
                sqlCommand.Connection.Open();

                var id = sqlCommand.ExecuteScalar();

                return Created($"api/animals/{id}", new CreateAnimalResponse((int)id, request));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, UpdateAnimalRequest request)
        {

            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand(
                    "UPDATE Animal SET Name = @1, Description = @2, Category = @3, Area = @4 WHERE IdAnimal = @5",
                    sqlConnection
                );
                sqlCommand.Parameters.AddWithValue("@1", request.Name);
                sqlCommand.Parameters.AddWithValue("@2", request.Description);
                sqlCommand.Parameters.AddWithValue("@3", request.Category);
                sqlCommand.Parameters.AddWithValue("@4", request.Area);
                sqlCommand.Parameters.AddWithValue("@5", id);
                sqlCommand.Connection.Open();

                var affectedRows = sqlCommand.ExecuteNonQuery();
                return affectedRows == 0 ? NotFound() : NoContent();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveStudent(int id)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @1", sqlConnection);
                command.Parameters.AddWithValue("@1", id);
                command.Connection.Open();

                var affectedRows = command.ExecuteNonQuery();

                return affectedRows == 0 ? NotFound() : NoContent();
            }
        }
    }
}
