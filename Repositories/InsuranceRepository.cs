using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using SharpCompress.Readers;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

namespace Repositories
{
    public class InsuranceRepository
    { 
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
                        cmd.Parameters.Add(new SqlParameter("@CustomerDocument", insurance.Customer.Document));
                        cmd.Parameters.Add(new SqlParameter("@Deductible", insurance.Deductible));
                        cmd.Parameters.Add(new SqlParameter("@CarLicensePlate", insurance.Car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@MainConductorDocument", insurance.MainConductor.Document));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Insurance.INSERT, new
                        {
                            CustomerDocument = insurance.Customer.Document,
                            insurance.Deductible,
                            CarLicensePlate = insurance.Car.LicensePlate,
                            MainConductorDocument = insurance.MainConductor.Document
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
                                    Customer = await new CustomerRepository().Get(reader.GetString(1), type),
                                    Deductible = reader.GetDecimal(2),
                                    Car = await new CarRepository().Get(reader.GetString(3), type),
                                    MainConductor = await new ConductorRepository().Get(reader.GetString(4), type)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(Insurance.GETALL).ToList();
                        foreach (var item in query)
                        {
                            // tive que usar esse metodo porque o Query do dapper aceita ate 7 elementos no maximo
                            list.Add(new Insurance
                            {
                                Id = item.Id,
                                Customer = await new CustomerRepository().Get(item.CustomerDocument, type),
                                Deductible = item.Deductible,
                                Car = await new CarRepository().Get(item.CarLicenseplate, type),
                                MainConductor = await new ConductorRepository().Get(item.MainConductorDocument, type)
                            });
                        }
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
                                    Customer = await new CustomerRepository().Get(reader.GetString(1), type),
                                    Deductible = reader.GetDecimal(2),
                                    Car = await new CarRepository().Get(reader.GetString(3), type),
                                    MainConductor = await new ConductorRepository().Get(reader.GetString(4), type)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(Insurance.GET).ToList();
                        foreach (var item in query)
                        {
                            list = new Insurance
                            {
                                Id = item.Id,
                                Customer = await new CustomerRepository().Get(item.CustomerDocument, type),
                                Deductible = item.Deductible,
                                Car = await new CarRepository().Get(item.CarLicenseplate, type),
                                MainConductor = await new ConductorRepository().Get(item.MainConductorDocument, type)
                            };
                        }
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
