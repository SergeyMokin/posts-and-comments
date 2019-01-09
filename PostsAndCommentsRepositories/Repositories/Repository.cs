using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsModels;
using PostsAndCommentsRepositories.DBContext;

namespace PostsAndCommentsRepositories.Repositories
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
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Edit(T model)
        {
            var entity = await _dbSet.FindAsync(model.Id)
                ?? throw new ArgumentNullException();

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
                ?? throw new ArgumentNullException();

            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
