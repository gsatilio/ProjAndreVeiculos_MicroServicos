using APIBank.Services;
using Models;
using Xunit;

namespace APIBank_Test
{
    public class UnitBank
    {
        [Fact]
        public void Test1()
        {
            for (int i = 0; i < 10; i++)
            {
                var msg = new Bank()
                {
                    BankName = "Santander",
                    CNPJ = "12345678900000",
                    FoundationDate = DateTime.Now
                };
                Bank msgOut = new BankRabbitService().PostBankToMongo(msg).Result;
            }

            Assert.True(true);
        }
    }
}