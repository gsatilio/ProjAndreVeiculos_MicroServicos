namespace MongoDatabase
{
    public class MongoDBAPIDataBaseSettings : IMongoDBAPIDataBaseSettings
    {
        public string AddressCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
