using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PixTypeRepository
    {
        private string Conn { get; set; }
        public PixTypeRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(PixType pixType, int type)
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
                        cmd.Parameters.Add(PixType.INSERT);
                        cmd.Parameters.Add(new SqlParameter("@Name", pixType.Name));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        result = db.ExecuteScalar<int>(PixType.INSERT, pixType);
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
