using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PixRepository
    {
        private string Conn { get; set; }
        public PixRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Pix pix, int type)
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
                        cmd.CommandText = Pix.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@IdPixType", pix.PixType.Id));
                        cmd.Parameters.Add(new SqlParameter("@PixKey", pix.PixKey));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Pix.INSERT, new { IdPixType = pix.PixType.Id, pix.PixKey });
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

        public async Task<List<Pix>> GetAll(int type)
        {
            List<Pix>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Pix.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Pix
                                {
                                    Id = reader.GetInt32(0),
                                    PixKey = reader.GetString(1),
                                    PixType = new PixType
                                    {
                                        Id = reader.GetInt32(2),
                                        Name = reader.GetString(3)
                                    }
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Pix, PixType, Pix>(Pix.GETALL,
                            (pix, pixType) =>
                            {
                                pix.PixType = pixType;
                                return pix;
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

        public async Task<Pix> Get(int id, int type)
        {
            Pix? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Pix.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdPix", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Pix
                                {
                                    Id = reader.GetInt32(0),
                                    PixKey = reader.GetString(1),
                                    PixType = new PixType
                                    {
                                        Id = reader.GetInt32(2),
                                        Name = reader.GetString(3)
                                    }
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Pix, PixType, Pix>(Pix.GET,
                            (pix, pixType) =>
                            {
                                pix.PixType = pixType;
                                return pix;
                            }, new { IdPix = id }, splitOn: "Id"
                    ).ToList().FirstOrDefault();
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
