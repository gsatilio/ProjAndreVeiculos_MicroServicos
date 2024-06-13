using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using SharpCompress.Readers;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class FinancialPendingRepository
    {
        private string Conn { get; set; }
        public FinancialPendingRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<int> Insert(FinancialPending financialPending, int type)
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
                        cmd.CommandText = FinancialPending.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Description", financialPending.Description));
                        cmd.Parameters.Add(new SqlParameter("@Value", financialPending.Value));
                        cmd.Parameters.Add(new SqlParameter("@PendingDate", financialPending.PendingDate));
                        cmd.Parameters.Add(new SqlParameter("@BillingDate", financialPending.BillingDate));
                        cmd.Parameters.Add(new SqlParameter("@Status", financialPending.Status));
                        cmd.Parameters.Add(new SqlParameter("@CustomerDocument", financialPending.Customer.Document));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(FinancialPending.INSERT, new
                        {
                            financialPending.Description,
                            financialPending.Value,
                            financialPending.PendingDate,
                            financialPending.BillingDate,
                            financialPending.Status,
                            CustomerDocument = financialPending.Customer.Document
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

        public async Task<List<FinancialPending>> GetAll(int type)
        {
            List<FinancialPending>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = FinancialPending.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new FinancialPending
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1),
                                    Value = reader.GetDecimal(2),
                                    PendingDate = reader.GetDateTime(3),
                                    BillingDate = reader.GetDateTime(4),
                                    Status = reader.GetBoolean(5),
                                    Customer = await new CustomerRepository().Get(reader.GetString(6), type)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(FinancialPending.GETALL).ToList();
                        foreach (var item in query)
                        {
                            list.Add(new FinancialPending
                            {
                                Id = item.Id,
                                Description = item.Description,
                                Value = item.Value,
                                PendingDate = item.PendingDate,
                                BillingDate = item.BillingDate,
                                Status = item.Status,
                                Customer = await new CustomerRepository().Get(item.Customer, type)
                            });
                        }
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

        public async Task<FinancialPending> Get(int id, int type)
        {
            FinancialPending? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = FinancialPending.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdFinancialPending", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new FinancialPending
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1),
                                    Value = reader.GetDecimal(1),
                                    PendingDate = reader.GetDateTime(2),
                                    BillingDate = reader.GetDateTime(3),
                                    Status = reader.GetBoolean(4),
                                    Customer = await new CustomerRepository().Get(reader.GetString(5), type)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(FinancialPending.GET, new { IdFinancialPending = id }).ToList();
                        foreach (var item in query)
                        {
                            list = new FinancialPending
                            {
                                Id = item.Id,
                                Description = item.Description,
                                Value = item.Value,
                                PendingDate = item.PendingDate,
                                BillingDate = item.BillingDate,
                                Status = item.Status,
                                Customer = await new CustomerRepository().Get(item.Customer, type)
                            };
                        }
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
