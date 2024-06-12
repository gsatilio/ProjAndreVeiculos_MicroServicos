using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Repositories
{
    public class CategoryRepository
    {
        private string Conn { get; set; }
        public CategoryRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public async Task<List<Category>> GetAll(int type)
        {
            List<Category>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {

                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Category.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Category
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Category>(Category.GETALL).ToList();
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

        public async Task<Category> Get(int id, int type)
        {
            Category? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Category.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdCategory", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Category
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1)
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Category>(Category.GET).ToList().FirstOrDefault();
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
