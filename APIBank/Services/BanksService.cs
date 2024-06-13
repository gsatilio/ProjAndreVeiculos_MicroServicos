using Models;
using MongoDB;
using MongoDB.Driver;
using Repositories;
using System.Net;

namespace APIBank.Services
{
    public class BanksService
    {
        private BankRepository _repository;
        private readonly IMongoCollection<Bank> _bank;
        public BanksService(IMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _bank = database.GetCollection<Bank>(settings.BankCollectionName);
        }
        public BanksService()
        {
            _repository = new BankRepository();
        }
        public Bank InsertMongo(Bank bank)
        {
            _bank.InsertOne(bank);
            return bank;
        }
        public List<Bank> GetAllMongo()
        {
            return _bank.Find(x => true).ToList();
        }
        public Bank GetMongoById(string cnpj)
        {
            return _bank.Find<Bank>(bank => bank.CNPJ == cnpj).FirstOrDefault();
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
