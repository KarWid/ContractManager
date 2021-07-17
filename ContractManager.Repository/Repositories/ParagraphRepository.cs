namespace ContractManager.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ContractManager.Repository.Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class ParagraphRepository : BaseRepository, IRepository<ParagraphEntity>
    {
        private readonly ContractManagerDbContext _dbContext;
        public ParagraphRepository(ContractManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IRepository{TEntity}"/>
        public async Task AddAsync(ParagraphEntity paragraphEntity)
        {
            paragraphEntity.Id = ObjectId.GenerateNewId().ToString();
            await _dbContext.Paragraphs.InsertOneAsync(paragraphEntity);
        }

        /// <inheritdoc cref="IRepository{TEntity}"/>
        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await _dbContext.Paragraphs.DeleteOneAsync(CreateIdFilter(id));
            return deleteResult.IsAcknowledged;
        }

        /// <inheritdoc cref="IRepository{TEntity}"/>
        public async Task<ParagraphEntity> GetAsync(string id)
        {
            return await _dbContext.Paragraphs.Find(_ => _.Id.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<List<ParagraphEntity>> GetListAsync()
        {
            return await _dbContext.Paragraphs.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity}"/>
        public async Task<ReplaceOneResult> ReplaceAsync(ParagraphEntity paragraphEntity)
        {
            return await _dbContext.Paragraphs.ReplaceOneAsync(CreateIdFilter(paragraphEntity.Id), paragraphEntity);
        }
    }
}
