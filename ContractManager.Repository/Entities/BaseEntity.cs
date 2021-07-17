namespace ContractManager.Repository.Entities
{
    using System;

    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
