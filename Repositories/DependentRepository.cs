using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class DependentRepository
    {
        private string Conn { get; set; }
        public DependentRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<int> Insert(Dependent dependent, int type)
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
                        cmd.CommandText = Dependent.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Document", dependent.Document));
                        cmd.Parameters.Add(new SqlParameter("@CustomerDocument", dependent.Customer.Document));
                        cmd.Parameters.Add(new SqlParameter("@Name", dependent.Name));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", dependent.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@AddressId", dependent.Address.Id));
                        cmd.Parameters.Add(new SqlParameter("@Phone", dependent.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", dependent.Email));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Dependent.INSERT, new
                        {
                            dependent.Document,
                            CustomerDocument = dependent.Customer.Document,
                            dependent.Name,
                            dependent.DateOfBirth,
                            AddressId = dependent.Address.Id,
                            dependent.Phone,
                            dependent.Email
                        });
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public async Task<List<Dependent>> GetAll(int type)
        {
            List<Dependent>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Dependent.GETALLGENERIC;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Dependent
                                {
                                    Document = reader.GetString(0),
                                    Customer = await new CustomerRepository().Get(reader.GetString(1), type),
                                    Name = reader.GetString(2),
                                    DateOfBirth = reader.GetDateTime(3),
                                    Address = await new AddressRepository().Get(reader.GetInt32(4), type),
                                    Phone = reader.GetString(5),
                                    Email = reader.GetString(6),
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Dependent, Address, Customer, Dependent>(Dependent.GETALL,
                            (dependent, addep, customer) =>
                            {
                                dependent.Address = addep;
                                dependent.Customer = customer;
                                return dependent;
                            }, splitOn: "Phone, Uf"
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

        public async Task<Dependent> Get(string document, int type)
        {
            Dependent? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Dependent.GETGENERIC;
                        cmd.Parameters.Add(new SqlParameter("@DependentDocument", document));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Dependent
                                {
                                    Document = reader.GetString(0),
                                    Customer = await new CustomerRepository().Get(reader.GetString(1), type),
                                    Name = reader.GetString(2),
                                    DateOfBirth = reader.GetDateTime(3),
                                    Address = await new AddressRepository().Get(reader.GetInt32(4), type),
                                    Phone = reader.GetString(5),
                                    Email = reader.GetString(6),
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Dependent, Address, Customer, Dependent>(Dependent.GETALL,
                            (dependent, addep, customer) =>
                            {
                                dependent.Address = addep;
                                dependent.Customer = customer;
                                return dependent;
                            }, splitOn: "Phone, Uf"
                    ).ToList().First();
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
