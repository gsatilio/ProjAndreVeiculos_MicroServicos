using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PixTypeRepository
    {
        private string Conn { get; set; }
        public PixTypeRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(PixType pixType, int type)
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
                        cmd.CommandText = PixType.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Name", pixType.Name));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        result = db.ExecuteScalar<int>(PixType.INSERT, pixType);
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

        public async Task<List<PixType>> GetAll(int type)
        {
            List<PixType>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = PixType.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PixType
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<PixType>(PixType.GETALL).ToList();
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
        public async Task<PixType> Get(int id, int type)
        {
            PixType? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = PixType.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdPixType", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new PixType
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<PixType>(PixType.GET, new { IdPixType = id }).ToList().FirstOrDefault();
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
