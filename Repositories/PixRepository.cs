using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PixRepository
    {
        private string Conn { get; set; }
        public PixRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Pix pix, int type)
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
                        cmd.CommandText = Pix.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@IdPixType", pix.PixType.Id));
                        cmd.Parameters.Add(new SqlParameter("@PixKey", pix.PixKey));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Pix.INSERT, new { IdPixType = pix.PixType.Id, pix.PixKey });
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
