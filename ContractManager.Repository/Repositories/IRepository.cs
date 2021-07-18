namespace ContractManager.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entity">New entity.</param>
        /// <returns>Newly created entity.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ReplaceOneResult> ReplaceAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();
    }
}
