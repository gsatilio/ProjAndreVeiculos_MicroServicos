namespace MongoDB
{
    public interface IMongoDBAPIDataBaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AddressCollectionName { get; set; }
    }
}
