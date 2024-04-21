

namespace LibraryManagementTask.Data.Repositories._BaseRepo
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
