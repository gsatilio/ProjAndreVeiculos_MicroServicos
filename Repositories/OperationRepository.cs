using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class OperationRepository
    {
        private string Conn { get; set; }
        public OperationRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Operation operation, int type)
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
                        cmd.CommandText = Operation.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Description", operation.Description));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Operation.INSERT, operation);
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
        public OperationList Retrieve()
        {
            OperationList opList = new OperationList();
            opList.Operation = new List<Operation>();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    var tc = db.Query(" SELECT Id, Description FROM OPERATION ");
                    foreach (var item in tc)
                    {
                        opList.Operation.Add(new Operation
                        {
                            Id = item.Id,
                            Description = item.Description
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
            return opList;
        }

        public async Task<List<Operation>> GetAll(int type)
        {
            List<Operation>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Operation.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Operation
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Operation>(Operation.GETALL).ToList();
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
        public async Task<Operation> Get(int id, int type)
        {
            Operation? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Operation.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdOperation", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Operation
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Operation>(Operation.GET, new { IdOperation = id }).ToList().FirstOrDefault();
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
