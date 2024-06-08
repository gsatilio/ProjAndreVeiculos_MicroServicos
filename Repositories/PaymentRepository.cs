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
                        cmd.Parameters.Add(Payment.INSERT);
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
    }
}
