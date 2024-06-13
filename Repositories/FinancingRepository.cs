using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Models;
using MongoDB;
using System.Configuration;
using System.Net;

namespace Repositories
{
    public class FinancingRepository
    {
        private string Conn { get; set; }

        public FinancingRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<int> Insert(Financing financing, int type)
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
                        cmd.CommandText = Financing.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@SaleId", financing.Sale.Id));
                        cmd.Parameters.Add(new SqlParameter("@FinancingDate", financing.FinancingDate));
                        cmd.Parameters.Add(new SqlParameter("@BankCNPJ", financing.Bank.CNPJ));
                        cmd.ExecuteNonQuery();
                        result = financing.Id;
                    }
                    else // Dapper
                    {
                        db.Execute(Financing.INSERT, financing);
                        result = financing.Id;
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

        public async Task<List<Financing>> GetAll(int type)
        {
            List<Financing>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Financing.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Financing
                                {
                                    Id = reader.GetInt32(0),
                                    Sale = await new SaleRepository().Get(reader.GetInt32(1), type),
                                    FinancingDate = reader.GetDateTime(2),
                                    Bank = await new BankRepository().Get(reader.GetString(3), type)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Financing>(Financing.GETALL).ToList();
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
        public async Task<Financing> Get(int id, int type)
        {
            Financing? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Financing.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdFinancing", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Financing
                                {
                                    Id = reader.GetInt32(0),
                                    Sale = await new SaleRepository().Get(reader.GetInt32(1), type),
                                    FinancingDate = reader.GetDateTime(2),
                                    Bank = await new BankRepository().Get(reader.GetString(3), type)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Financing>(Financing.GET, new { FinancingId = id }).ToList().FirstOrDefault();
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
