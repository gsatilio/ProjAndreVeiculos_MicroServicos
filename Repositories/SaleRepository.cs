using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

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
                        cmd.Parameters.Add(new SqlParameter("@IdPayment", sale.Payment.Id));
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
                            IdPayment = sale.Payment.Id
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
    }
}
