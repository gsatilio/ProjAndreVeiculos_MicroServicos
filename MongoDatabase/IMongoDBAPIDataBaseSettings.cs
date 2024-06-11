namespace MongoDatabase
{
    public interface IMongoDBAPIDataBaseSettings
    {
        string AddressCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
