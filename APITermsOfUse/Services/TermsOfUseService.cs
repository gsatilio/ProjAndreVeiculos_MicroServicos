using Models;
using MongoDB.Driver;
using MongoDB;

namespace APITermsOfUse.Services
{
    public class TermsOfUseService
    {
        private readonly IMongoCollection<TermsOfUse> _tos;
        public TermsOfUseService(IMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tos = database.GetCollection<TermsOfUse>(settings.TermsOfUseCollectionName);
        }
        public TermsOfUse InsertMongo(TermsOfUse tos)
        {
            var last = GetAllMongo().LastOrDefault();
            int id = 0;
            if (last != null)
                id = last.Id;

            tos.Id = id + 1;
            _tos.InsertOne(tos);
            return tos;
        }
        public List<TermsOfUse> GetAllMongo()
        {
            return _tos.Find(x => true).ToList();
        }
        public TermsOfUse GetMongoById(int id)
        {
            return _tos.Find<TermsOfUse>(tos => tos.Id == id).FirstOrDefault();
        }
    }
}
