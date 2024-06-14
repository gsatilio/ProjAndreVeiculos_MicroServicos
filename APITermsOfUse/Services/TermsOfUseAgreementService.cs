using Models;
using Models.DTO;
using MongoDB.Driver;
using MongoDB;
using Newtonsoft.Json;
using Repositories;

namespace APITermsOfUse.Services
{
    public class TermsOfUseAgreementService
    {
        private readonly IMongoCollection<TermsOfUseAgreement> _tos;
        public TermsOfUseAgreementService(IMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tos = database.GetCollection<TermsOfUseAgreement>(settings.TermsOfUseAgreementCollectionName);
        }

        public TermsOfUseAgreement InsertMongo(TermsOfUseAgreement tos)
        {
            var last = GetAllMongo().LastOrDefault();
            int id = 0;
            if (last != null)
                id = last.Id;

            tos.Id = id + 1;
            _tos.InsertOne(tos);
            return tos;
        }
        public List<TermsOfUseAgreement> GetAllMongo()
        {
            return _tos.Find(x => true).ToList();
        }
        public TermsOfUseAgreement GetMongoById(int id)
        {
            return _tos.Find<TermsOfUseAgreement>(tos => tos.Id == id).FirstOrDefault();
        }
    }
}
