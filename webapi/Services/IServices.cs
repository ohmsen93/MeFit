namespace webapi.Services
{
    public interface IServices<T, Id>
    {
        /// <summary>
        /// Retrives all instances from the database.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetAll();
        /// <summary>
        /// Retrives a particulat instance from the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(Id id);
        /// <summary>
        /// Inserts a new row into the database based on the parameter.
        /// </summary>
        /// <param name="entity"></param>
        Task<T> Create(T entity);
        /// <summary>
        /// Updates an existing row based on the provided parameters.
        /// </summary>
        /// <param name="entity"></param>
        Task<T> Update(T entity);
        /// <summary>
        /// Deletes a record by its ID.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteById(Id id);
    }
}
