using System.Data.SqlClient;
using PKOL.DTOs;

namespace PKOL.Endpoints
{
    public static class GroupsDapperEndpoints
    {
        public static void RegisterGroupsDapperEndpoints(this WebApplication app)
        {
            var groups = app.MapGroup("api/groups");

            groups.MapGet("/{id:int}", GetStudents);
        }

        private static async Task<IResult> GetStudents(IConfiguration configuration, int id)
        {
            using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                await sqlConnection.OpenAsync();

                var command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = """
                    SELECT ID, Nam, Students_ID FROM Groups
                    INNER JOIN GroupAssignments ON ID = Groups_ID
                    WHERE ID = @Id;
                """;

                command.Parameters.AddWithValue("@Id", id);
                var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows) return Results.NotFound($"Group with id:{id} does not exits");

                await reader.ReadAsync();

                GetGroupDTO result = new GetGroupDTO(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        !await reader.IsDBNullAsync(2) ? [reader.GetInt32(2)] : []
                );

                while(await reader.ReadAsync())
                {
                    result.StudentIds.Add(reader.GetInt32(2));
                }

                return Results.Ok(result);
            }
        }
    }
}