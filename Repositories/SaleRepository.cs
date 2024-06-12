using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;

namespace Repositories
{
    public class SaleRepository
    {
        private string Conn { get; set; }
        public SaleRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Sale sale, int type)
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
                        cmd.CommandText = Sale.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@LicensePlate", sale.Car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@SaleDate", sale.SaleDate));
                        cmd.Parameters.Add(new SqlParameter("@SaleValue", sale.SaleValue));
                        cmd.Parameters.Add(new SqlParameter("@Customer", sale.Customer.Document));
                        cmd.Parameters.Add(new SqlParameter("@Employee", sale.Employee.Document));
                        cmd.Parameters.Add(new SqlParameter("@IdSale", sale.Id));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        db.Execute(Sale.INSERT, new
                        {
                            sale.Car.LicensePlate,
                            sale.SaleDate,
                            sale.SaleValue,
                            Customer = sale.Customer.Document,
                            Employee = sale.Employee.Document,
                            IdSale = sale.Id
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

        public async Task<List<Sale>> GetAll(int type)
        {
            List<Sale>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Sale.GETALLGENERIC;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Sale
                                {
                                    Id = reader.GetInt32(0),
                                    Car = await new CarRepository().Get(reader.GetString(1), type),
                                    SaleDate = reader.GetDateTime(2),
                                    SaleValue = reader.GetDecimal(3),
                                    Customer = await new CustomerRepository().Get(reader.GetString(4), type),
                                    Employee = await new EmployeeRepository().Get(reader.GetString(5), type),
                                    Payment = await new PaymentRepository().Get(reader.GetInt32(6), type)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(Sale.GETALLGENERIC).ToList();
                        foreach (var item in query)
                        {
                            // tive que usar esse metodo porque o Query do dapper aceita ate 7 elementos no maximo
                            list.Add(new Sale
                            {
                                Id = item.Id,
                                Car = await new CarRepository().Get(item.CarLicensePlate, type),
                                SaleDate = item.SaleDate,
                                SaleValue = item.SaleValue,
                                Customer = await new CustomerRepository().Get(item.CustomerDocument, type),
                                Employee = await new EmployeeRepository().Get(item.EmployeeDocument, type),
                                Payment = await new PaymentRepository().Get(item.PaymentId, type)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
            return list;
        }

        public async Task<Sale> Get(int id, int type)
        {
            Sale? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Sale.GETGENERIC;
                        cmd.Parameters.Add(new SqlParameter("@IdSale", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Sale
                                {
                                    Id = reader.GetInt32(0),
                                    Car = await new CarRepository().Get(reader.GetString(1), type),
                                    SaleDate = reader.GetDateTime(2),
                                    SaleValue = reader.GetDecimal(3),
                                    Customer = await new CustomerRepository().Get(reader.GetString(4), type),
                                    Employee = await new EmployeeRepository().Get(reader.GetString(5), type),
                                    Payment = await new PaymentRepository().Get(reader.GetInt32(6), type)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(Sale.GETGENERIC, new { IdSale = id }).ToList();
                        foreach (var item in query)
                        {
                            // tive que usar esse metodo porque o Query do dapper aceita ate 7 elementos no maximo
                            list = new Sale
                            {
                                Id = item.Id,
                                Car = await new CarRepository().Get(item.CarLicensePlate, type),
                                SaleDate = item.SaleDate,
                                SaleValue = item.SaleValue,
                                Customer = await new CustomerRepository().Get(item.CustomerDocument, type),
                                Employee = await new EmployeeRepository().Get(item.EmployeeDocument, type),
                                Payment = await new PaymentRepository().Get(item.PaymentId, type)
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
