using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CarOperationRepository
    {
        private string Conn { get; set; }
        public CarOperationRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }
        public int Insert(CarOperation carOp, int type)
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
                        cmd.CommandText = CarOperation.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@CarLicensePlate", carOp.Car.LicensePlate));
                        cmd.Parameters.Add(new SqlParameter("@OperationId", carOp.Operation.Id));
                        cmd.Parameters.Add(new SqlParameter("@Status", carOp.Status));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(CarOperation.INSERT, new
                        {
                            CarLicensePlate = carOp.Car.LicensePlate,
                            OperationId = carOp.Operation.Id,
                            carOp.Status
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
        public int ChangeStatusCarServiceTable(CarOperation carOp, int type)
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
                        cmd.CommandText = (CarOperation.UPDATE);
                        cmd.Parameters.Add(new SqlParameter("@Id", carOp.Id));
                        cmd.Parameters.Add(new SqlParameter("@Status", carOp.Status));
                        cmd.ExecuteNonQuery();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(CarOperation.UPDATE, new
                        {
                            Id = carOp.Id,
                            Status = carOp.Status
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
        public CarOperationList Retrieve(int type)
        {
            CarOperationList csList = new CarOperationList();
            csList.CarOperation = new List<CarOperation>();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = ("SELECT Id, CarLicensePlate, OperationId, Status FROM CAROPERATION");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                csList.CarOperation.Add(new CarOperation
                                {
                                    Id = reader.GetInt32(0),
                                    Car = RetrieveCar(reader.GetString(1), type),
                                    Operation = RetrieveService(reader.GetInt32(2), type),
                                    Status = reader.GetBoolean(3)
                                });
                            }
                            reader.Close();
                        }
                    }
                    else // Dapper
                    {
                        var tc = db.Query(" SELECT Id, CarLicensePlate, OperationId, Status FROM CAROPERATION ");
                        foreach (var item in tc)
                        {
                            csList.CarOperation.Add(new CarOperation
                            {
                                Id = item.Id,
                                Status = item.Status,
                                Car = RetrieveCar(item.CarLicensePlate, type),
                                Operation = RetrieveService(item.OperationId, type)
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
            return csList;
        }

        public Car RetrieveCar(string licensePlate, int type)
        {
            Car car = new Car();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = ($"SELECT LicensePlate, Name, ModelYear, FabricationYear, Color FROM CAR WHERE LicensePlate = '{licensePlate}'");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                car = new Car
                                {
                                    LicensePlate = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    ModelYear = reader.GetInt32(2),
                                    FabricationYear = reader.GetInt32(3),
                                    Color = reader.GetString(4)
                                };
                            }
                            reader.Close();
                        }
                    }
                    else // Dapper
                    {
                        var tc = db.Query($" SELECT LicensePlate, Name, ModelYear, FabricationYear, Color FROM CAR WHERE LicensePlate = '{licensePlate}' ");
                        foreach (var item in tc)
                        {
                            car = new Car
                            {
                                LicensePlate = item.LicensePlate,
                                Name = item.Name,
                                ModelYear = item.ModelYear,
                                FabricationYear = item.FabricationYear,
                                Color = item.Color
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
            return car;
        }

        public Operation RetrieveService(int Id, int type)
        {
            Operation serv = new Operation();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = ($" SELECT Id, Description FROM OPERATION WHERE Id = {Id} ");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                serv = new Operation
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1)
                                };
                            }
                            reader.Close();
                        }
                    }
                    else // Dapper
                    {
                        var tc = db.Query($" SELECT Id, Description FROM OPERATION WHERE Id = {Id} ");
                        foreach (var item in tc)
                        {
                            serv = new Operation
                            {
                                Id = item.Id,
                                Description = item.Description
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
            return serv;
        }

        public CarOperationList RetrieveCarServiceTableStatus(bool status, int type)
        {
            CarOperationList csList = new CarOperationList();
            csList.CarOperation = new List<CarOperation>();
            int auxstatus = 0;
            if (status)
                auxstatus = 1;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = ($" SELECT Id, CarLicensePlate, OperationId, Status FROM CAROPERATION WHERE Status = {auxstatus}");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                csList.CarOperation.Add(new CarOperation
                                {
                                    Id = reader.GetInt32(0),
                                    Car = RetrieveCar(reader.GetString(1), type),
                                    Operation = RetrieveService(reader.GetInt32(2), type),
                                    Status = reader.GetBoolean(3),
                                });
                            }
                            reader.Close();
                        }
                    }
                    else // Dapper
                    {
                        var tc = db.Query($" SELECT Id, CarLicensePlate, OperationId, Status FROM CAROPERATION WHERE Status = {auxstatus}");
                        foreach (var item in tc)
                        {
                            csList.CarOperation.Add(new CarOperation
                            {
                                Id = item.Id,
                                Status = item.Status,
                                Car = RetrieveCar(item.CarLicensePlate, type),
                                Operation = RetrieveService(item.OperationId, type)
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
            return csList;
        }
    }
}
