using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class CreditCardRepository
    {
        private string Conn { get; set; }
        public CreditCardRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(CreditCard creditCard, int type)
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
                        cmd.CommandText = CreditCard.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@CardNumber", creditCard.CardNumber));
                        cmd.Parameters.Add(new SqlParameter("@SecurityCode", creditCard.SecurityCode));
                        cmd.Parameters.Add(new SqlParameter("@ExpirationDate", creditCard.ExpirationDate));
                        cmd.Parameters.Add(new SqlParameter("@CardName", creditCard.CardName));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(CreditCard.INSERT, creditCard);
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

        public async Task<List<CreditCard>> GetAll(int type)
        {
            List<CreditCard>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = CreditCard.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new CreditCard
                                {
                                    Id = reader.GetInt32(0),
                                    CardNumber = reader.GetString(1),
                                    SecurityCode = reader.GetString(2),
                                    ExpirationDate = reader.GetString(3),
                                    CardName = reader.GetString(4)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<CreditCard>(CreditCard.GETALL).ToList();
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
        public async Task<CreditCard> Get(int id, int type)
        {
            CreditCard? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = CreditCard.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdCreditCard", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new CreditCard
                                {
                                    Id = reader.GetInt32(0),
                                    CardNumber = reader.GetString(1),
                                    SecurityCode = reader.GetString(2),
                                    ExpirationDate = reader.GetString(3),
                                    CardName = reader.GetString(4)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<CreditCard>(CreditCard.GET, new { IdCreditCard = id }).ToList().FirstOrDefault();
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
