using Dapper;
using FluentValidation;
using LAB06.DTOs;
using System.Data.SqlClient;

namespace LAB06.Endpoints
{
    public static class AnimalsDapperEndpoints
    {
        public static void RegisterStudentsDapperEndpoints(this WebApplication app)
        {
            var students = app.MapGroup("dapi/animals");

            students.MapGet("/", GetAnimals);
            students.MapPost("/", CreateAnimal);
            students.MapDelete("{id:int}", RemoveAnimal);
            students.MapPut("{id:int}", UpdateAnimal);
        }

        private static IResult UpdateAnimal(IConfiguration configuration, IValidator<UpdateAnimalRequest> validator, int id, UpdateAnimalRequest request)
        {

            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var affectedRows = sqlConnection.Execute(
                    "UPDATE Animal SET Name = @1, Description = @2, Category = @3, Area = @4 WHERE IdAnimal = @5",
                    new
                    {
                        FirstName = request.Name,
                        LastName = request.Description,
                        Phone = request.Category,
                        Birthdate = request.Area,
                        Id = id
                    }
                );

                if (affectedRows == 0) return Results.NotFound();
            }

            return Results.NoContent();
        }

        private static IResult RemoveAnimal(IConfiguration configuration, int id)
        {
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var affectedRows = sqlConnection.Execute(
                    "DELETE FROM animal WHERE IdAnimal = @Id",
                    new { Id = id }
                );
                return affectedRows == 0 ? Results.NotFound() : Results.NoContent();
            }
        }

        private static IResult CreateAnimal(IConfiguration configuration, IValidator<CreateAnimalRequest> validator, CreateAnimalRequest request)
        {
            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var id = sqlConnection.ExecuteScalar<int>(
                    "INSERT INTO animal (Name, Description, Category, Area) values (@1, @2, @3, @4); SELECT CAST(SCOPE_IDENTITY() as int)",
                    new
                    {
                        FirstName = request.Name,
                        LastName = request.Description,
                        Phone = request.Category,
                        Birthdate = request.Area
                    }
                );

                return Results.Created($"dapi/animals/{id}", new CreateAnimalResponse(id, request));
            }
        }

        private static IResult GetAnimals(IConfiguration configuration)
        {
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var students = sqlConnection.Query<GetAnimalsResponse>("SELECT * FROM animal");
                return Results.Ok(students);
            }
        }
    }
}
