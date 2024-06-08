using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class RoleRepository
    {
        private string Conn { get; set; }
        public RoleRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Role role, int type)
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
                        cmd.CommandText = Role.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Number", role.Description));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Role.INSERT, role);
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
