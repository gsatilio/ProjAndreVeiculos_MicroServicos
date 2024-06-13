using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using MongoDB.Bson;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class DriverLicenseRepository
    {
        private string Conn { get; set; }
        public DriverLicenseRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }
        public async Task<int> Insert(DriverLicense driverLicense, int type)
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
                        cmd.CommandText = DriverLicense.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@DriverId", driverLicense.DriverId));
                        cmd.Parameters.Add(new SqlParameter("@DueDate", driverLicense.DueDate));
                        cmd.Parameters.Add(new SqlParameter("@RG", driverLicense.RG));
                        cmd.Parameters.Add(new SqlParameter("@CPF", driverLicense.CPF));
                        cmd.Parameters.Add(new SqlParameter("@MotherName", driverLicense.MotherName));
                        cmd.Parameters.Add(new SqlParameter("@FatherName", driverLicense.FatherName));
                        cmd.Parameters.Add(new SqlParameter("@CategoryId", driverLicense.Category.Id));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(DriverLicense.INSERT, new
                        {
                            driverLicense.DriverId,
                            driverLicense.DueDate,
                            driverLicense.RG,
                            driverLicense.CPF,
                            driverLicense.MotherName,
                            driverLicense.FatherName,
                            CategoryId = driverLicense.Category.Id
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
        public async Task<List<DriverLicense>> GetAll(int type)
        {
            List<DriverLicense>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = DriverLicense.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new DriverLicense
                                {
                                    DriverId = reader.GetInt64(0),
                                    DueDate = reader.GetDateTime(1),
                                    RG = reader.GetString(2),
                                    CPF = reader.GetString(3),
                                    MotherName = reader.GetString(4),
                                    FatherName = reader.GetString(5),
                                    Category = await new CategoryRepository().Get(reader.GetInt32(6), type)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<DriverLicense, Category, DriverLicense>(DriverLicense.GETALL,
                            (driverLicense, category) =>
                            {
                                driverLicense.Category = category;
                                return driverLicense;
                            }, splitOn: "Id"
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

        public async Task<DriverLicense> Get(long id, int type)
        {
            DriverLicense? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = DriverLicense.GET;
                        cmd.Parameters.Add(new SqlParameter("@DriverId", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new DriverLicense
                                {
                                    DriverId = reader.GetInt64(0),
                                    DueDate = reader.GetDateTime(1),
                                    RG = reader.GetString(2),
                                    CPF = reader.GetString(3),
                                    MotherName = reader.GetString(4),
                                    FatherName = reader.GetString(5),
                                    Category = await new CategoryRepository().Get(reader.GetInt32(6), type)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<DriverLicense, Category, DriverLicense>(DriverLicense.GET,
                            (driverLicense, category) =>
                            {
                                driverLicense.Category = category;
                                return driverLicense;
                            }, splitOn: "Id"
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
