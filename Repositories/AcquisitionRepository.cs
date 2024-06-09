using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

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
                        cmd.CommandText = Acquisition.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@LicensePlate", acquisition.Car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@Price", acquisition.Price));
                        cmd.Parameters.Add(new SqlParameter("@AcquisitionDate", acquisition.AcquisitionDate));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Acquisition.INSERT, new
                        {
                            acquisition.Car.LicensePlate,
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

        public async Task<List<Acquisition>> GetAll(int type)
        {
            List<Acquisition>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Acquisition.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Acquisition
                                {
                                    Id = reader.GetInt32(0),
                                    Car = new Car
                                    {
                                        LicensePlate = reader.GetString(3),
                                        Name = reader.GetString(4),
                                        ModelYear = reader.GetInt32(5),
                                        FabricationYear = reader.GetInt32(6),
                                        Color = reader.GetString(7),
                                        Sold = reader.GetBoolean(8)
                                    },
                                    Price = reader.GetDecimal(1),
                                    AcquisitionDate = reader.GetDateTime(2),
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Acquisition, Car, Acquisition>(Acquisition.GETALL,
                            (acquisition, car) =>
                            {
                                acquisition.Car = car;
                                return acquisition;
                            }, splitOn: "LicensePlate"
                    ).ToList();
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
            return list;
        }

        public async Task<Acquisition> Get(int id, int type)
        {
            Acquisition? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Acquisition.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdAcquisition", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Acquisition
                                {
                                    Id = reader.GetInt32(0),
                                    Car = new Car
                                    {
                                        LicensePlate = reader.GetString(3),
                                        Name = reader.GetString(4),
                                        ModelYear = reader.GetInt32(5),
                                        FabricationYear = reader.GetInt32(6),
                                        Color = reader.GetString(7),
                                        Sold = reader.GetBoolean(8)
                                    },
                                    Price = reader.GetDecimal(1),
                                    AcquisitionDate = reader.GetDateTime(2),
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Acquisition, Car, Acquisition>(Acquisition.GET,
                            (acquisition, car) =>
                            {
                                acquisition.Id = id;
                                acquisition.Car = car;
                                return acquisition;
                            }, new { IdAcquisition = id }, splitOn: "LicensePlate"
                    ).ToList().First();
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
            return list;
        }

    }
}
