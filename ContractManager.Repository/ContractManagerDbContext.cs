namespace ContractManager.Repository
{
    using MongoDB.Driver;
    using ContractManager.Repository.Entities;

    public class ContractManagerDbContext 
    {
        private readonly IMongoDatabase _db;

        public ContractManagerDbContext(IMongoClient client, string dbName)
        {
            _db = client.GetDatabase(dbName);
        }

        public IMongoCollection<ContractEntity> Contracts => _db.GetCollection<ContractEntity>(Constants.MongoDocuments.CONTRACT);
        public IMongoCollection<ParagraphEntity> Paragraphs => _db.GetCollection<ParagraphEntity>(Constants.MongoDocuments.PARAGRAPH);
    }
}
