using Microsoft.EntityFrameworkCore;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task Add(T t)
        {
            _dbContext.Set<T>().Add(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.ID == id);

        }

        public async Task Update(T t)
        {
            _dbContext.Set<T>().Update(t);
            await _dbContext.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                }
            }
        }

    }
}

