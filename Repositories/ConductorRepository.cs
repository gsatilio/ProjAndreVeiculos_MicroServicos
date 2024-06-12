using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class ConductorRepository
    {
        private string Conn { get; set; }
        public ConductorRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }
        public int Insert(Conductor conductor, int type)
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
                        cmd.CommandText = Conductor.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Document", conductor.Document));
                        cmd.Parameters.Add(new SqlParameter("@CNHDriverLicense", conductor.DriverLicense.DriverId));
                        cmd.Parameters.Add(new SqlParameter("@Name", conductor.Name));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", conductor.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@AddressId", conductor.Address.Id));
                        cmd.Parameters.Add(new SqlParameter("@Phone", conductor.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", conductor.Email));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Conductor.INSERT, new
                        {
                            conductor.Document,
                            conductor.DriverLicense.DriverId,
                            conductor.Name,
                            conductor.DateOfBirth,
                            conductor.Address.Id,
                            conductor.Phone,
                            conductor.Email
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

        public async Task<List<Conductor>> GetAll(int type)
        {
            List<Conductor>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Conductor.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Conductor
                                {
                                    Document = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    DateOfBirth = reader.GetDateTime(2),
                                    Phone = reader.GetString(3),
                                    Email = reader.GetString(4),
                                    DriverLicense = await new DriverLicenseRepository().Get(reader.GetInt32(5), type),
                                    Address = await new AddressRepository().Get(reader.GetInt32(13), type)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Conductor, DriverLicense, Category, Address, Conductor>(Conductor.GETALL,
                            (conductor, cnh, category, address) =>
                            {
                                conductor.Address = address;
                                conductor.DriverLicense = cnh;
                                conductor.DriverLicense.Category = category;
                                return conductor;
                            }, splitOn: "Email, FatherName, Id"
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

        public async Task<Conductor> Get(int id, int type)
        {
            Conductor? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Conductor.GET;
                        cmd.Parameters.Add(new SqlParameter("@Document", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Conductor
                                {
                                    Document = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    DateOfBirth = reader.GetDateTime(2),
                                    Phone = reader.GetString(3),
                                    Email = reader.GetString(4),
                                    DriverLicense = await new DriverLicenseRepository().Get(reader.GetInt32(5), type),
                                    Address = await new AddressRepository().Get(reader.GetInt32(13), type)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Conductor, DriverLicense, Category, Address, Conductor>(Conductor.GETALL,
                            (conductor, cnh, category, address) =>
                            {
                                conductor.Address = address;
                                conductor.DriverLicense = cnh;
                                conductor.DriverLicense.Category = category;
                                return conductor;
                            }, splitOn: "Email, FatherName, Id"
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
