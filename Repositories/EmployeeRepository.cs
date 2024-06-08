using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;

namespace Repositories
{
    public class EmployeeRepository
    {
        private string Conn { get; set; }
        public EmployeeRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Employee employee, int type)
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
                        cmd.CommandText = Employee.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@Document", employee.Document));
                        cmd.Parameters.Add(new SqlParameter("@RoleId", employee.Role.Id));
                        cmd.Parameters.Add(new SqlParameter("@ComissionValue", employee.ComissionValue));
                        cmd.Parameters.Add(new SqlParameter("@Comission", employee.Comission));
                        cmd.Parameters.Add(new SqlParameter("@Name", employee.Name));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", employee.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@AddressId", employee.Address.Id));
                        cmd.Parameters.Add(new SqlParameter("@Phone", employee.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", employee.Email));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        db.Execute(Employee.INSERT, new
                        {
                            employee.Document,
                            RoleId = employee.Role.Id,
                            employee.ComissionValue,
                            employee.Comission,
                            employee.Name,
                            employee.DateOfBirth,
                            AddressId = employee.Address.Id,
                            employee.Phone,
                            employee.Email,
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

        public async Task<List<Employee>> GetAll(int type)
        {
            List<Employee>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Employee.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Employee
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
                                    Name = reader.GetString(3),
                                    Comission = reader.GetDecimal(4),
                                    ComissionValue = reader.GetDecimal(5),
                                    Phone = reader.GetString(6),
                                    Role = new Role
                                    {
                                        Id = reader.GetInt32(16),
                                        Description = reader.GetString(17),
                                    }
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Employee, Address, Role, Employee>(Employee.GETALL,
                            (employee, address, role) =>
                            {
                                employee.Address = address;
                                employee.Role = role;
                                return employee;
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

        public async Task<Employee> Get(string document, int type)
        {
            Employee? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Employee.GETALL + " WHERE A.Document = @document";
                        cmd.Parameters.Add(new SqlParameter("@document", document));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Employee
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
                                    Name = reader.GetString(3),
                                    Comission = reader.GetDecimal(4),
                                    ComissionValue = reader.GetDecimal(5),
                                    Phone = reader.GetString(6),
                                    Role = new Role
                                    {
                                        Id = reader.GetInt32(16),
                                        Description = reader.GetString(17),
                                    }
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Employee, Address, Role, Employee>(Employee.GETALL,
                            (employee, address, role) =>
                            {
                                employee.Address = address;
                                employee.Role = role;
                                return employee;
                            }, splitOn: "Phone, Uf"
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
