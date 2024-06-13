using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Models;
using System.Configuration;
using System.Net;

namespace Repositories
{
    public class BankRepository
    {
        private string Conn { get; set; }
        public BankRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<string> Insert(Bank bank, int type)
        {
            string result = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Bank.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@CNPJ", bank.CNPJ));
                        cmd.Parameters.Add(new SqlParameter("@BankName", bank.BankName));
                        cmd.Parameters.Add(new SqlParameter("@FoundationDate", bank.FoundationDate));
                        cmd.ExecuteNonQuery();
                        result = bank.CNPJ;
                    }
                    else // Dapper
                    {
                        db.Execute(Bank.INSERT, bank);
                        result = bank.CNPJ;
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

        public async Task<List<Bank>> GetAll(int type)
        {
            List<Bank>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Bank.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Bank
                                {
                                    CNPJ = reader.GetString(0),
                                    BankName = reader.GetString(1),
                                    FoundationDate = reader.GetDateTime(2)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Bank>(Bank.GETALL).ToList();
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
        public async Task<Bank> Get(string cnpj, int type)
        {
            Bank? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Bank.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdBank", cnpj));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Bank
                                {
                                    CNPJ = reader.GetString(0),
                                    BankName = reader.GetString(1),
                                    FoundationDate = reader.GetDateTime(2)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Bank>(Bank.GET, new { CNPJ = cnpj }).ToList().FirstOrDefault();
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
