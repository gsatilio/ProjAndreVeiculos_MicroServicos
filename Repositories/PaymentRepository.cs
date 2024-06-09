using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class PaymentRepository
    {
        private string Conn { get; set; }
        public PaymentRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(Payment payment, int type)
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
                        cmd.CommandText = Payment.INSERT;
                        cmd.Parameters.Add(new SqlParameter("@IdCreditCard", payment.CreditCard.Id));
                        cmd.Parameters.Add(new SqlParameter("@IdBoleto", payment.Boleto.Id));
                        cmd.Parameters.Add(new SqlParameter("@IdPix", payment.Pix.Id));
                        cmd.Parameters.Add(new SqlParameter("@PaymentDate", payment.PaymentDate));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(Payment.INSERT, new
                        {
                            IdCreditCard = payment.CreditCard.Id,
                            IdBoleto = payment.Boleto.Id,
                            IdPix = payment.Pix.Id,
                            payment.PaymentDate
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

        public async Task<List<Payment>> GetAll(int type)
        {
            List<Payment>? list = new();
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Payment.GETALL;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Payment
                                {
                                    Id = reader.GetInt32(0),
                                    PaymentDate = reader.GetDateTime(1),
                                    CreditCard = new CreditCard
                                    {
                                        Id = reader.GetInt32(2),
                                        CardName = reader.GetString(3),
                                        CardNumber = reader.GetString(4),
                                        ExpirationDate = reader.GetString(5),
                                        SecurityCode = reader.GetString(6)
                                    },
                                    Boleto = new Boleto
                                    {
                                        Id = reader.GetInt32(7),
                                        ExpirationDate = reader.GetDateTime(8),
                                        Number = reader.GetInt32(9)
                                    },
                                    Pix = new Pix
                                    {
                                        Id = reader.GetInt32(10),
                                        PixKey = reader.GetString(11),
                                        PixType = new PixType
                                        {
                                            Id = reader.GetInt32(12),
                                            Name = reader.GetString(13)
                                        }
                                    }
                                });
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Payment, CreditCard, Boleto, Pix, PixType, Payment>(Payment.GETALL,
                            (payment, creditCard, boleto, pix, pixType) =>
                            {
                                payment.CreditCard = creditCard;
                                payment.Boleto = boleto;
                                payment.Pix = pix;
                                payment.Pix.PixType = pixType;
                                return payment;
                            }, splitOn: "Id, Id, Id, Id"
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

        public async Task<Payment> Get(int id, int type)
        {
            Payment? list = null;
            try
            {
                using (var db = new SqlConnection(Conn))
                {
                    db.Open();
                    if (type == 0) // ADO.NET
                    {
                        var cmd = new SqlCommand { Connection = db };
                        cmd.CommandText = Payment.GET;
                        cmd.Parameters.Add(new SqlParameter("@IdPayment", id));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list = new Payment
                                {
                                    Id = reader.GetInt32(0),
                                    PaymentDate = reader.GetDateTime(1),
                                    CreditCard = new CreditCard
                                    {
                                        Id = reader.GetInt32(2),
                                        CardName = reader.GetString(3),
                                        CardNumber = reader.GetString(4),
                                        ExpirationDate = reader.GetString(5),
                                        SecurityCode = reader.GetString(6)
                                    },
                                    Boleto = new Boleto
                                    {
                                        Id = reader.GetInt32(7),
                                        ExpirationDate = reader.GetDateTime(8),
                                        Number = reader.GetInt32(9)
                                    },
                                    Pix = new Pix
                                    {
                                        Id = reader.GetInt32(10),
                                        PixKey = reader.GetString(11),
                                        PixType = new PixType
                                        {
                                            Id = reader.GetInt32(12),
                                            Name = reader.GetString(13)
                                        }
                                    }
                                };
                            }
                        }
                    }
                    else // Dapper
                    {
                        list = db.Query<Payment, CreditCard, Boleto, Pix, PixType, Payment>(Payment.GET,
                            (payment, card, boleto, pix, pixType) =>
                            {
                                payment.CreditCard = card; 
                                payment.Boleto = boleto;
                                payment.Pix = pix;
                                payment.Pix.PixType = pixType;
                                return payment;
                            }, new { IdPayment = id }, splitOn: "Id"
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
