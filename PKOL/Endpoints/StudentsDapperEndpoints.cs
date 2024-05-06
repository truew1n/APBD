using System.Data.SqlClient;

namespace PKOL.Endpoints
{
    public static class StudentsDapperEndpoints
    {
        public static void RegisterStudentsDapperEndpoints(this WebApplication app)
        {
            var students = app.MapGroup("api/students");

            students.MapDelete("{id:int}", DeleteStudent);
        }

        private static async Task<IResult> DeleteStudent(IConfiguration configuration, int id)
        {
            using(var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                await sqlConnection.OpenAsync();
                await using var transaction = await sqlConnection.BeginTransactionAsync();
                try
                {
                    var command0 = new SqlCommand();
                    command0.Connection = sqlConnection;
                    command0.CommandText = """
                        DELETE FROM GroupAssignments WHERE Students_ID = @StudentId;
                    """;
                    command0.Transaction = (SqlTransaction) transaction;
                    command0.Parameters.AddWithValue("@StudentId", id);
                    await command0.ExecuteNonQueryAsync();

                    var command1 = new SqlCommand();
                    command1.Connection = sqlConnection;
                    command1.CommandText = """
                        DELETE FROM Students WHERE ID = @StudentId;
                    """;
                    command1.Transaction = (SqlTransaction) transaction;
                    command1.Parameters.AddWithValue("@StudentId", id);
                    var affectedRows = await command1.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();

                    if (affectedRows == 0) return Results.NotFound($"Student with id: {id} does not exist");
                } catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
            
            return Results.NoContent();
        }
    }
}
