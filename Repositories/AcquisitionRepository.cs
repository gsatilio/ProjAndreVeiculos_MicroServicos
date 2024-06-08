using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Diagnostics;

namespace Repositories
{
    public class AcquisitionRepository
    {
        private string Conn { get; set; }
        public AcquisitionRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Acquisition acquisition, int type)
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
                        cmd.Parameters.Add(Acquisition.INSERT);
                        cmd.Parameters.Add(new SqlParameter("@LicensePlate", acquisition.Car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@Price", acquisition.Price));
                        cmd.Parameters.Add(new SqlParameter("@AcquisitionDate", acquisition.AcquisitionDate));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Acquisition.INSERT, new
                        {
                            LicensePlate = acquisition.Car.LicensePlate,
                            acquisition.Price,
                            acquisition.AcquisitionDate
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
