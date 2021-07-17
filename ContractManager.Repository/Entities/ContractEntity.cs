namespace ContractManager.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class ContractEntity : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public IEnumerable<Guid> ParagraphIds { get; set; }
    }
}
