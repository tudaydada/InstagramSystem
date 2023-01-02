using InstagramSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace InstagramSystem.Repositories
{
    public interface IBaseRepository<T> where T : class
    {

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// GetById 
        /// </summary>
        /// <returns></returns>
        Task<T> GetByIdAsync(int Id);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void DeleteAsync(T entity);

        /// <summary>
        /// Save
        /// </summary>
        void Save();
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        #region private method Ensure Entity Not Null 
        /// <summary>
        /// Ensure entity isn't null
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void EnsureEntityNotNull(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
        #endregion

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> InsertAsync(T entity)
        {
            EnsureEntityNotNull(entity);

            await context.Set<T>().AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            EnsureEntityNotNull(entity);

            context.Set<T>().Update(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public async void DeleteAsync(T entity)
        {
            EnsureEntityNotNull(entity);

            context.Set<T>().Remove(entity);

        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToArrayAsync();
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
