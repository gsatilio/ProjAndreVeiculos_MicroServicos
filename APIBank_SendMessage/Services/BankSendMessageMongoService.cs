using Models;
using MongoDB;
using MongoDB.Driver;
using Repositories;

namespace APIBank_Mongo.Services
{
    public class BankSendMessageMongoService
    {
        private BankRepository _repository;
        private readonly IMongoCollection<Bank> _bank;
        public BankSendMessageMongoService(IMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _bank = database.GetCollection<Bank>(settings.BankCollectionName);
            _repository = new BankRepository();
        }
        public BankSendMessageMongoService()
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

    }
}
