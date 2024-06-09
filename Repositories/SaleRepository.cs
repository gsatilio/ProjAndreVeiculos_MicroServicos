using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;
using System.Data;

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
                        cmd.Parameters.Add(new SqlParameter("@IdSale", sale.Id));
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
                            IdSale = sale.Id
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

        public async Task<List<Sale>> GetAll(int type)
        {
            List<Sale>? list = new();
            List<SaleQuery>? lista = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Sale.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Sale
                                {
                                    Id = reader.GetInt32(0),
                                    SaleDate = reader.GetDateTime(1),
                                    SaleValue = reader.GetDecimal(2),
                                    Car = new Car
                                    {
                                        LicensePlate = reader.GetString(3),
                                        Color = reader.GetString(4),
                                        FabricationYear = reader.GetInt32(5),
                                        ModelYear = reader.GetInt32(6),
                                        Name = reader.GetString(7),
                                        Sold = reader.GetBoolean(8),
                                    },
                                    Customer = new Customer
                                    {
                                        Document = reader.GetString(9),
                                        DateOfBirth = reader.GetDateTime(10),
                                        Email = reader.GetString(11),
                                        Income = reader.GetDecimal(12),
                                        Name = reader.GetString(13),
                                        PDFDocument = reader.GetString(14),
                                        Phone = reader.GetString(15),
                                        Address = new Address
                                        {
                                            Id = reader.GetInt32(16),
                                            CEP = reader.GetString(17),
                                            City = reader.GetString(18),
                                            Complement = reader.GetString(19),
                                            Neighborhood = reader.GetString(20),
                                            Number = reader.GetInt32(21),
                                            Street = reader.GetString(22),
                                            StreetType = reader.GetString(23),
                                            Uf = reader.GetString(24)
                                        }
                                    },
                                    Employee = new Employee
                                    {
                                        Document = reader.GetString(25),
                                        Comission = reader.GetDecimal(26),
                                        ComissionValue = reader.GetDecimal(27),
                                        DateOfBirth = reader.GetDateTime(28),
                                        Email = reader.GetString(29),
                                        Name = reader.GetString(30),
                                        Phone = reader.GetString(31),
                                        Address = new Address
                                        {
                                            Id = reader.GetInt32(32),
                                            CEP = reader.GetString(33),
                                            City = reader.GetString(34),
                                            Complement = reader.GetString(35),
                                            Neighborhood = reader.GetString(36),
                                            Number = reader.GetInt32(37),
                                            Street = reader.GetString(38),
                                            StreetType = reader.GetString(39),
                                            Uf = reader.GetString(40)
                                        },
                                        Role = new Role
                                        {
                                            Id = reader.GetInt32(41),
                                            Description = reader.GetString(42)
                                        }
                                    },
                                    Payment = new Payment
                                    {
                                        Id = reader.GetInt32(43),
                                        PaymentDate = reader.GetDateTime(44),
                                        CreditCard = new CreditCard
                                        {
                                            Id = reader.GetInt32(45),
                                            CardName = reader.GetString(46),
                                            CardNumber = reader.GetString(47),
                                            ExpirationDate = reader.GetString(48),
                                            SecurityCode = reader.GetString(49)
                                        },
                                        Boleto = new Boleto
                                        {
                                            Id = reader.GetInt32(50),
                                            ExpirationDate = reader.GetDateTime(51),
                                            Number = reader.GetInt32(52)
                                        },
                                        Pix = new Pix
                                        {
                                            Id = reader.GetInt32(53),
                                            PixKey = reader.GetString(54),
                                            PixType = new PixType
                                            {
                                                Id = reader.GetInt32(55),
                                                Name = reader.GetString(56)
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(Sale.GETALLGENERIC).ToList();
                        foreach (var item in query)
                        {
                            // tive que usar esse metodo porque o Query do dapper aceita ate 7 elementos no maximo
                            list.Add(new Sale
                            {
                                Id = item.Id,
                                Car = await new CarRepository().Get(item.CarLicensePlate, type),
                                SaleDate = item.SaleDate,
                                SaleValue = item.SaleValue,
                                Customer = await new CustomerRepository().Get(item.CustomerDocument, type),
                                Employee = await new EmployeeRepository().Get(item.EmployeeDocument, type),
                                Payment = await new PaymentRepository().Get(item.PaymentId, type)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
            return list;
        }

        public async Task<Sale> Get(int id, int type)
        {
            Sale? list = null;
            SaleQuery lista = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Sale.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdSale", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Sale
                                {
                                    Id = reader.GetInt32(0),
                                    SaleDate = reader.GetDateTime(1),
                                    SaleValue = reader.GetDecimal(2),
                                    Car = new Car
                                    {
                                        LicensePlate = reader.GetString(3),
                                        Color = reader.GetString(4),
                                        FabricationYear = reader.GetInt32(5),
                                        ModelYear = reader.GetInt32(6),
                                        Name = reader.GetString(7),
                                        Sold = reader.GetBoolean(8),
                                    },
                                    Customer = new Customer
                                    {
                                        Document = reader.GetString(9),
                                        DateOfBirth = reader.GetDateTime(10),
                                        Email = reader.GetString(11),
                                        Income = reader.GetDecimal(12),
                                        Name = reader.GetString(13),
                                        PDFDocument = reader.GetString(14),
                                        Phone = reader.GetString(15),
                                        Address = new Address
                                        {
                                            Id = reader.GetInt32(16),
                                            CEP = reader.GetString(17),
                                            City = reader.GetString(18),
                                            Complement = reader.GetString(19),
                                            Neighborhood = reader.GetString(20),
                                            Number = reader.GetInt32(21),
                                            Street = reader.GetString(22),
                                            StreetType = reader.GetString(23),
                                            Uf = reader.GetString(24)
                                        }
                                    },
                                    Employee = new Employee
                                    {
                                        Document = reader.GetString(25),
                                        Comission = reader.GetDecimal(26),
                                        ComissionValue = reader.GetDecimal(27),
                                        DateOfBirth = reader.GetDateTime(28),
                                        Email = reader.GetString(29),
                                        Name = reader.GetString(30),
                                        Phone = reader.GetString(31),
                                        Address = new Address
                                        {
                                            Id = reader.GetInt32(32),
                                            CEP = reader.GetString(33),
                                            City = reader.GetString(34),
                                            Complement = reader.GetString(35),
                                            Neighborhood = reader.GetString(36),
                                            Number = reader.GetInt32(37),
                                            Street = reader.GetString(38),
                                            StreetType = reader.GetString(39),
                                            Uf = reader.GetString(40)
                                        },
                                        Role = new Role
                                        {
                                            Id = reader.GetInt32(41),
                                            Description = reader.GetString(42)
                                        }
                                    },
                                    Payment = new Payment
                                    {
                                        Id = reader.GetInt32(43),
                                        PaymentDate = reader.GetDateTime(44),
                                        CreditCard = new CreditCard
                                        {
                                            Id = reader.GetInt32(45),
                                            CardName = reader.GetString(46),
                                            CardNumber = reader.GetString(47),
                                            ExpirationDate = reader.GetString(48),
                                            SecurityCode = reader.GetString(49)
                                        },
                                        Boleto = new Boleto
                                        {
                                            Id = reader.GetInt32(50),
                                            ExpirationDate = reader.GetDateTime(51),
                                            Number = reader.GetInt32(52)
                                        },
                                        Pix = new Pix
                                        {
                                            Id = reader.GetInt32(53),
                                            PixKey = reader.GetString(54),
                                            PixType = new PixType
                                            {
                                                Id = reader.GetInt32(55),
                                                Name = reader.GetString(56)
                                            }
                                        }
                                    }
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        var query = db.Query(Sale.GETGENERIC, new { IdSale = id }).ToList();
                        foreach (var item in query)
                        {
                            // tive que usar esse metodo porque o Query do dapper aceita ate 7 elementos no maximo
                            list = new Sale
                            {
                                Id = item.Id,
                                Car = await new CarRepository().Get(item.CarLicensePlate, type),
                                SaleDate = item.SaleDate,
                                SaleValue = item.SaleValue,
                                Customer = await new CustomerRepository().Get(item.CustomerDocument, type),
                                Employee = await new EmployeeRepository().Get(item.EmployeeDocument, type),
                                Payment = await new PaymentRepository().Get(item.PaymentId, type)
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
            return list;
        }
    }
}
