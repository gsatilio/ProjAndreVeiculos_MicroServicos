using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class InsuranceRepository
    { /*
        private string Conn { get; set; }
        public InsuranceRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Insurance insurance, int type)
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
                        cmd.CommandText = Insurance.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@LicensePlate", insurance.Car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@CustomerDocument", insurance.Customer.Document));
                        cmd.Parameters.Add(new SqlParameter("@Deductible", insurance.Deductible));
                        cmd.Parameters.Add(new SqlParameter("@InsuranceDate", insurance.InsuranceDate));
                        cmd.Parameters.Add(new SqlParameter("@InsuranceDate", insurance.InsuranceDate));
                        cmd.Parameters.Add(new SqlParameter("@InsuranceDate", insurance.InsuranceDate));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Insurance.INSERT, new
                        {
                            insurance.Car.LicensePlate,
                            insurance.Price,
                            insurance.InsuranceDate
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

        public async Task<List<Insurance>> GetAll(int type)
        {
            List<Insurance>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Insurance.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Insurance
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
                                    InsuranceDate = reader.GetDateTime(2),
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Insurance, Car, Insurance>(Insurance.GETALL,
                            (insurance, car) =>
                            {
                                insurance.Car = car;
                                return insurance;
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

        public async Task<Insurance> Get(int id, int type)
        {
            Insurance? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Insurance.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdInsurance", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Insurance
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
                                    InsuranceDate = reader.GetDateTime(2),
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Insurance, Car, Insurance>(Insurance.GET,
                            (insurance, car) =>
                            {
                                insurance.Id = id;
                                insurance.Car = car;
                                return insurance;
                            }, new { IdInsurance = id }, splitOn: "LicensePlate"
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
        */
    }
}
