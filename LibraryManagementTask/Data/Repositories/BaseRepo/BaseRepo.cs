using Microsoft.EntityFrameworkCore;

namespace LibraryManagementTask.Data.Repositories._BaseRepo
{
    public class BaseRepo<TEntity>: IBaseRepo<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(TEntity entity)
            => _context.Add(entity);

        public void Update(TEntity entity)
            => _context.Update(entity);

        public void Delete(TEntity entity)
            => _context.Remove(entity);

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();
    }
}
