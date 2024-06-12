using Azure;
using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;

namespace Repositories
{
    public class AddressRepository
    {
        private string Conn { get; set; }
        public AddressRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<int> Insert(Address address, int type)
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
                        cmd.CommandText = Address.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Street", address.Street));
                        cmd.Parameters.Add(new SqlParameter("@CEP", address.CEP));
                        cmd.Parameters.Add(new SqlParameter("@Neighborhood", address.Neighborhood));
                        cmd.Parameters.Add(new SqlParameter("@StreetType", address.StreetType));
                        cmd.Parameters.Add(new SqlParameter("@Complement", address.Complement));
                        cmd.Parameters.Add(new SqlParameter("@Number", address.Number));
                        cmd.Parameters.Add(new SqlParameter("@Uf", address.Uf));
                        cmd.Parameters.Add(new SqlParameter("@City", address.City));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Address.INSERT, address);
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
        public async Task<List<Address>> GetAll(int type)
        {
            List<Address>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Address.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Address
                                {
                                    Street = reader.GetString(0),
                                    CEP = reader.GetString(1),
                                    Neighborhood = reader.GetString(2),
                                    StreetType = reader.GetString(3),
                                    Complement = reader.GetString(4),
                                    Number = reader.GetInt32(5),
                                    Uf = reader.GetString(6),
                                    City = reader.GetString(7),
                                    Id = reader.GetInt32(8)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Address>(Address.GETALL).ToList();
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
        public async Task<Address> Get(int id, int type)
        {
            Address? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Address.GET;
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Address
                                {
                                    Street = reader.GetString(0),
                                    CEP = reader.GetString(1),
                                    Neighborhood = reader.GetString(2),
                                    StreetType = reader.GetString(3),
                                    Complement = reader.GetString(4),
                                    Number = reader.GetInt32(5),
                                    Uf = reader.GetString(6),
                                    City = reader.GetString(7),
                                    Id = reader.GetInt32(8)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Address>(Address.GET, new { Id = id }).ToList().FirstOrDefault();
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
