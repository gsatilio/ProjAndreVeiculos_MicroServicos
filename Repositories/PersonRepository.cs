using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PersonRepository
    {
        private string Conn { get; set; }
        public PersonRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert((Address, DateOnly, string, string, string, string) person, int type)
        {
            int result = 0;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.Parameters.Add(Address.INSERT);
                        cmd.Parameters.Add(new SqlParameter("@Document", person.Item1.Id));
                        cmd.Parameters.Add(new SqlParameter("@Name", person.Item2));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", person.Item3));
                        cmd.Parameters.Add(new SqlParameter("@IdAddress", person.Item4));
                        cmd.Parameters.Add(new SqlParameter("@Phone", person.Item5));
                        cmd.Parameters.Add(new SqlParameter("@Email", person.Item6));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        db.Execute(" INSERT INTO PERSON (Document, Name, DateOfBirth, IdAddress, Phone, Email)" +
                        "VALUES (@Document, @Name, @DateOfBirth, @IdAddress, @Phone, @Email)", new
                        {
                            Document = person.Item1.Id,
                            Name = person.Item2,
                            DateOfBirth = person.Item3,
                            IdAddress = person.Item4,
                            Phone = person.Item5,
                            Email = person.Item6
                        });
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
            return result;
        }
    }
}
