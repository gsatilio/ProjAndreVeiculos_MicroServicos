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
                        /*cmd.Parameters.Add(Employee.INSERTPERSON);
                        cmd.Parameters.Add(new SqlParameter("@Number", employee.Address.Id));
                        cmd.Parameters.Add(new SqlParameter("@Document", employee.Document));
                        cmd.Parameters.Add(new SqlParameter("@Name", employee.Name));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", employee.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@Phone", employee.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", employee.Email));
                        cmd.ExecuteNonQuery();
                        */
                        cmd = new SqlCommand { Connection = db };
                        cmd.Parameters.Add(Employee.INSERT);
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
                        /*db.Execute(Customer.INSERTPERSON, new
                        {
                            IdAddress = employee.Address.Id,
                            employee.Document,
                            employee.Name,
                            employee.DateOfBirth,
                            employee.Phone,
                            employee.Email
                        });*/
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
    }
}
