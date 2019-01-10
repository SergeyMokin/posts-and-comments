using System.Linq;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsRepositories.DbContext;
using Company.PostsAndCommentsRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.PostsAndCommentsRepositories.Repositories
{
    public class Repository<T>: IRepository<T>
    where T: class, IModel<T>
    {
        private readonly DbSet<T> _dbSet;
        private readonly PacDbContext _dbContext;
        public Repository(PacDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _dbSet;
        }

        public async Task<T> Get(int id)
        {
            return (await _dbSet.FindAsync(id))
                   ?? throw new PacNotFoundException();
        }

        public async Task<T> Edit(T model)
        {
            var entity = await _dbSet.FindAsync(model.Id)
                ?? throw new PacNotFoundException();

            entity.Edit(model);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Add(T model)
        {
            _dbSet.Add(model);

            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id)
                ?? throw new PacNotFoundException();

            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
