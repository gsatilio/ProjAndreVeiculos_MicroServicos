using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System.Configuration;

namespace Repositories
{
    public class CreditCardRepository
    {
        private string Conn { get; set; }
        public CreditCardRepository()
        {
            Conn = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        }

        public int Insert(CreditCard creditCard, int type)
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
                        cmd.Parameters.Add(CreditCard.INSERT);
                        cmd.Parameters.Add(new SqlParameter("@CardNumber", creditCard.CardNumber));
                        cmd.Parameters.Add(new SqlParameter("@SecurityCode", creditCard.SecurityCode));
                        cmd.Parameters.Add(new SqlParameter("@ExpirationDate", creditCard.ExpirationDate));
                        cmd.Parameters.Add(new SqlParameter("@CardName", creditCard.CardName));
                        result = (int)cmd.ExecuteScalar();
                    }
                    else // Dapper
                    {
                        result = db.ExecuteScalar<int>(CreditCard.INSERT, creditCard);
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
