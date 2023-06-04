
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using System.Linq.Expressions;

namespace MvcMovie.DAL
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private MovieContext _context;
        private DbSet<T> table = null;

        public GenericRepository(MovieContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }


        public void Delete(int id)
        {
            T obj = table.Find(id);
            table.Remove(obj);
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = table;
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            return query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetByID(int id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        /*public void Save()
        {
            _context.SaveChanges();
        }*/

        public void Update(T obj)
        {
            table.Update(obj);
        }
    }
}
