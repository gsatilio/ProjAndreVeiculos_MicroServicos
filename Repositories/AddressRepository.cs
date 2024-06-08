﻿using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace Repositories
{
    public class AddressRepository
    {
        private string Conn { get; set; }
        public AddressRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Address address, int type)
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
                        cmd.Parameters.Add(Address.INSERT);
                        cmd.Parameters.Add(new SqlParameter("@Street", address.Street));
                        cmd.Parameters.Add(new SqlParameter("@CEP", address.CEP));
                        cmd.Parameters.Add(new SqlParameter("@Neighborhood", address.Neighborhood));
                        cmd.Parameters.Add(new SqlParameter("@StreetType", address.StreetType));
                        cmd.Parameters.Add(new SqlParameter("@Complement", address.Complement));
                        cmd.Parameters.Add(new SqlParameter("@Number", address.Number));
                        cmd.Parameters.Add(new SqlParameter("@Uf", address.Uf));
                        cmd.Parameters.Add(new SqlParameter("@City", address.City));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Address.INSERT, address);
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
