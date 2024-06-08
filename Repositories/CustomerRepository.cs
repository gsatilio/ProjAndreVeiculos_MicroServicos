using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;

namespace Repositories
{
    public class CustomerRepository
    {
        private string Conn { get; set; }
        public CustomerRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Customer customer, int type)
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
                        /*
                        cmd.Parameters.Add(Customer.INSERTPERSON);
                        cmd.Parameters.Add(new SqlParameter("@Number", customer.Address.Id));
                        cmd.Parameters.Add(new SqlParameter("@Document", customer.Document));
                        cmd.Parameters.Add(new SqlParameter("@Name", customer.Name));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", customer.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", customer.Email));
                        cmd.ExecuteNonQuery();
                        */
                        cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Customer.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Document", customer.Document));
                        cmd.Parameters.Add(new SqlParameter("@Income", customer.Income));
                        cmd.Parameters.Add(new SqlParameter("@PDFDocument", customer.PDFDocument));
                        cmd.Parameters.Add(new SqlParameter("@Name", customer.Name));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", customer.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@AddressId", customer.Address.Id));
                        cmd.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", customer.Email));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        /*db.Execute(Customer.INSERTPERSON, new
                        {
                            IdAddress = customer.Address.Id,
                            customer.Document,
                            customer.Name,
                            customer.DateOfBirth,
                            customer.Phone,
                            customer.Email
                        });*/
                        db.Execute(Customer.INSERT, customer);
                        result = db.ExecuteScalar<int>(Customer.INSERT, new
                        {
                            customer.Document,
                            customer.Income,
                            customer.PDFDocument,
                            customer.Name,
                            customer.DateOfBirth,
                            AddressId = customer.Address.Id,
                            customer.Phone,
                            customer.Email
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
