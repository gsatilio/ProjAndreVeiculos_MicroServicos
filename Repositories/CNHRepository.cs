using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class CNHRepository
    {
        private string Conn { get; set; }
        public CNHRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<List<CNH>> GetAll(int type)
        {
            List<CNH>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = CNH.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new CNH
                                {
                                    DriverLicense = reader.GetInt64(0),
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
                        list = db.Query<CNH, Category, CNH>(CNH.GETALL,
                            (cnh, category) =>
                            {
                                cnh.Category = category;
                                return cnh;
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

        public async Task<CNH> Get(int id, int type)
        {
            CNH? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = CNH.GET;
                        cmd.Parameters.Add(new SqlParameter("@DriverLicense", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new CNH
                                {
                                    DriverLicense = reader.GetInt64(0),
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
                        list = db.Query<CNH, Category, CNH>(CNH.GET,
                            (cnh, category) =>
                            {
                                cnh.Category = category;
                                return cnh;
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
