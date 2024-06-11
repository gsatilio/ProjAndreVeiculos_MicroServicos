namespace MongoDB
{
    public class MongoDBAPIDataBaseSettings : IMongoDBAPIDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AddressCollectionName { get; set; }
    }
}
