namespace MongoDB
{
    public interface IMongoDBAPIDataBaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AddressCollectionName { get; set; }
        string BankCollectionName { get; set; }
        string TermsOfUseAgreementCollectionName { get; set; }
        string TermsOfUseCollectionName { get; set; }
    }
}
