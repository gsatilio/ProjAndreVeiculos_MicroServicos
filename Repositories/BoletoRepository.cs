using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Models;
using System.Configuration;
using System.Net;

namespace Repositories
{
    public class BoletoRepository
    {
        private string Conn { get; set; }
        public BoletoRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Boleto boleto, int type)
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
                        cmd.CommandText = Boleto.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Number", boleto.Number));
                        cmd.Parameters.Add(new SqlParameter("@ExpirationDate", boleto.ExpirationDate));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Boleto.INSERT, boleto);
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

        public async Task<List<Boleto>> GetAll(int type)
        {
            List<Boleto>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Boleto.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Boleto
                                {
                                    Id = reader.GetInt32(0),
                                    Number = reader.GetInt32(1),
                                    ExpirationDate = reader.GetDateTime(2)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Boleto>(Boleto.GETALL).ToList();
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
        public async Task<Boleto> Get(int id, int type)
        {
            Boleto? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Boleto.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdBoleto", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Boleto
                                {
                                    Id = reader.GetInt32(0),
                                    Number = reader.GetInt32(1),
                                    ExpirationDate = reader.GetDateTime(2)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Boleto>(Boleto.GET, new { IdBoleto = id }).ToList().FirstOrDefault();
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
