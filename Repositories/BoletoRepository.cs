using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Models;
using System.Configuration;
using System.Net;

namespace Repositories
{
    public class BoletoRepository
    {
        private string Conn { get; set; }
        public BoletoRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Boleto boleto, int type)
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
                        cmd.Parameters.Add(Boleto.INSERT);
                        cmd.Parameters.Add(new SqlParameter("@Number", boleto.Number));
                        cmd.Parameters.Add(new SqlParameter("@ExpirationDate", boleto.ExpirationDate));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Boleto.INSERT, boleto);
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
