namespace MongoDB
{
    public class MongoDBAPIDataBaseSettings : IMongoDBAPIDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AddressCollectionName { get; set; }
        public string BankCollectionName { get; set; }
        public string TermsOfUseAgreementCollectionName { get; set; }
        public string TermsOfUseCollectionName { get; set; }

    }
}
