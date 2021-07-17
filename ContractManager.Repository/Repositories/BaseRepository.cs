namespace ContractManager.Repository.Repositories
{
    using MongoDB.Bson;

    public class BaseRepository
    {
        protected BsonDocument CreateIdFilter(string id)
        {
            return new BsonDocument("_id", new BsonObjectId(new ObjectId(id)));
        }
    }
}