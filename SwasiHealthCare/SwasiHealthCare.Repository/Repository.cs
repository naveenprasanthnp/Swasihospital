using SwasiHealthCare.Data.DbContext;
using SwasiHealthCare.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public SwasiHealthCareDbConext dbContext = null;

        private readonly DbSet<T> dbSet = null;
        public Repository()
        {
            this.dbContext = new SwasiHealthCareDbConext();
            this.dbContext.Configuration.ProxyCreationEnabled = false;
            // Enabled Lazy Loading concept
            this.dbContext.Configuration.LazyLoadingEnabled = true;
            dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Find the data using primary key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<T> GetById(long Id)
        {
            try
            {
                return await dbSet.FindAsync(Id);
            }
            catch
            {
                throw;
            }
        }

        public T GetByIds(long Id)
        {
            try
            {
                return  dbSet.Find(Id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// List all the value of entity
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<T> GetAllList()
        {
            try
            {
                return dbSet.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add new value to the entity
        /// </summary>
        /// <param name="entity"></param>
        public async Task Insert(T entity)
        {
            try
            {
                dbSet.Add(entity);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update the entity value
        /// </summary>
        /// <param name="entity"></param>
        public async Task Update(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Remove the data from entity 
        /// </summary>
        /// <param name="Id"></param>
        public async Task Delete(object Id)
        {
            try
            {
                T dataExists = dbSet.Find(Id);
                dbSet.Remove(dataExists);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveRange(IEnumerable<T> entity)
        {
            try
            {
                dbSet.RemoveRange(entity);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
