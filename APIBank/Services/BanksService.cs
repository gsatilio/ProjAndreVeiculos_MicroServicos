using Models;
using MongoDB;
using MongoDB.Driver;
using Repositories;
using System.Net;

namespace APIBank.Services
{
    public class BanksService
    {
        private readonly IMongoCollection<Bank> _bank;
        private BankRepository _repository = new();

        public BanksService(IMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _bank = database.GetCollection<Bank>(settings.BankCollectionName);
        }
        public void InsertMongo(Bank bank)
        {
            if (_bank != null)
                _bank.InsertOne(bank);
        }
        public async Task<string> Insert(Bank acquisition, int type)
        {
            string result = null;
            try
            {
                result = await _repository.Insert(acquisition, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
        public async Task<List<Bank>> GetAll(int type)
        {
            List<Bank> list = new List<Bank>();
            try
            {
                list = await _repository.GetAll(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }
        public async Task<Bank> Get(string cnpj, int type)
        {
            Bank list = new Bank();
            try
            {
                list = await _repository.Get(cnpj, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }
    }
}
