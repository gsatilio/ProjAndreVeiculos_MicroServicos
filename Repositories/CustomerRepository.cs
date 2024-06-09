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

        public async Task<List<Customer>> GetAll(int type)
        {
            List<Customer>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Customer.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Customer
                                {
                                    Address = new Address
                                    {
                                        CEP = reader.GetString(7),
                                        City = reader.GetString(8),
                                        Complement = reader.GetString(9),
                                        Id = reader.GetInt32(10),
                                        Neighborhood = reader.GetString(11),
                                        Number = reader.GetInt32(12),
                                        Street = reader.GetString(13),
                                        StreetType = reader.GetString(14),
                                        Uf = reader.GetString(15),
                                    },
                                    DateOfBirth = reader.GetDateTime(0),
                                    Document = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Income = reader.GetDecimal(3),
                                    Name = reader.GetString(4),
                                    PDFDocument = reader.GetString(5),
                                    Phone = reader.GetString(6)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Customer, Address, Customer>(Customer.GETALL,
                            (customer, address) =>
                            {
                                customer.Address = address;
                                return customer;
                            }, splitOn: "Phone"
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

        public async Task<Customer> Get(string document, int type)
        {
            Customer? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Customer.GET;
                        cmd.Parameters.Add(new SqlParameter("@Document", document));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Customer
                                {
                                    Address = new Address
                                    {
                                        CEP = reader.GetString(7),
                                        City = reader.GetString(8),
                                        Complement = reader.GetString(9),
                                        Id = reader.GetInt32(10),
                                        Neighborhood = reader.GetString(11),
                                        Number = reader.GetInt32(12),
                                        Street = reader.GetString(13),
                                        StreetType = reader.GetString(14),
                                        Uf = reader.GetString(15),
                                    },
                                    DateOfBirth = reader.GetDateTime(0),
                                    Document = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Income = reader.GetDecimal(3),
                                    Name = reader.GetString(4),
                                    PDFDocument = reader.GetString(5),
                                    Phone = reader.GetString(6)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Customer, Address, Customer>(Customer.GET,
                            (customer, address) =>
                            {
                                customer.Address = address;
                                return customer;
                            }, new {Document = document}, splitOn: "Phone"
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
