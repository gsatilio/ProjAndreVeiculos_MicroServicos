﻿using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Drawing;

namespace Repositories
{
    public class CarRepository
    {
        private string Conn { get; set; }
        public CarRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }
        public bool InsertBatch(CarList carList, int type)
        {
            var result = false;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        foreach (var item in carList.Car)
                        {
                            var cmd = new SqlCommand { Connection = db };
                            cmd.CommandText = Car.INSERT;
                            cmd.Parameters.Add(new SqlParameter("@LicensePlate", item.LicensePlate));
                            cmd.Parameters.Add(new SqlParameter("@Name", item.Name));
                            cmd.Parameters.Add(new SqlParameter("@ModelYear", item.ModelYear));
                            cmd.Parameters.Add(new SqlParameter("@FabricationYear", item.FabricationYear));
                            cmd.Parameters.Add(new SqlParameter("@Color", item.Color));
                            cmd.Parameters.Add(new SqlParameter("@Sold", false));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else // Dapper
                    {
                        foreach (var item in carList.Car)
                        {
                            db.Execute(Car.INSERT, item);
                        }
                    }
                    db.Close();
                    result = true;
                }
                result = true;
            }
            catch
            {
                result = false;
                throw;
            }
            return result;
        }

        public async Task<string> Insert(Car car, int type)
        {
            string result = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Car.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@LicensePlate", car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@Name", car.Name));
                        cmd.Parameters.Add(new SqlParameter("@ModelYear", car.ModelYear));
                        cmd.Parameters.Add(new SqlParameter("@FabricationYear", car.FabricationYear));
                        cmd.Parameters.Add(new SqlParameter("@Color", car.Color));
                        cmd.Parameters.Add(new SqlParameter("@Sold", car.Sold));
                        cmd.ExecuteNonQuery();
                        result = car.LicensePlate;
                    }
                    else // Dapper
                    {
                        db.Execute(Car.INSERT, car);
                        result = car.LicensePlate;
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

        public bool Delete(int type)
        {
            var result = false;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = " DELETE FROM CarOperation; DELETE FROM Car ";
                        cmd.ExecuteNonQuery();
                    }
                    else // Dapper
                    {
                        db.Execute(" DELETE FROM CarOperation; DELETE FROM Car ");
                    }
                    db.Close();
                    result = true;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }
            return result;
        }
        public CarList Retrieve(int type)
        {
            CarList carList = new CarList();
            carList.Car = new List<Car>();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = (Car.GETALL);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                carList.Car.Add(new Car
                                {
                                    LicensePlate = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    ModelYear = reader.GetInt32(2),
                                    FabricationYear = reader.GetInt32(3),
                                    Color = reader.GetString(4)
                                });
                            }
                            reader.Close();
                        }
                    }
                    else // Dapper
                    {
                        var tc = db.Query(Car.GETALL);
                        foreach (var item in tc)
                        {
                            carList.Car.Add(new Car
                            {
                                LicensePlate = item.LicensePlate,
                                Name = item.Name,
                                ModelYear = item.ModelYear,
                                FabricationYear = item.FabricationYear,
                                Color = item.Color
                            });
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
            return carList;
        }
        public async Task<List<Car>> GetAll(int type)
        {
            List<Car>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Car.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Car
                                {
                                    LicensePlate = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    ModelYear = reader.GetInt32(2),
                                    FabricationYear = reader.GetInt32(3),
                                    Color = reader.GetString(4),
                                    Sold = reader.GetBoolean(5)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Car>(Car.GETALL).ToList();
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
        public async Task<Car> Get(string licensePlate, int type)
        {
            Car? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Car.GET;
                        cmd.Parameters.Add(new SqlParameter("@LicensePlate", licensePlate));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = (new Car
                                {
                                    LicensePlate = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    ModelYear = reader.GetInt32(2),
                                    FabricationYear = reader.GetInt32(3),
                                    Color = reader.GetString(4),
                                    Sold = reader.GetBoolean(5)
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Car>(Car.GET, new { LicensePlate = licensePlate }).ToList().FirstOrDefault();
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
