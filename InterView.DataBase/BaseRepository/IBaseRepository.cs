using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.BaseRepository
{
    public interface IBaseRepository<T>
    {
        void Add(T item);

        Task AddAsync(T item);

        void AddRange(IEnumerable<T> items);

        Task AddRangeAsync(IEnumerable<T> items);

        void Delete(object key);

        void Delete(Expression<Func<T, bool>> where);

        Task DeleteAsync(object key);

        Task DeleteAsync(Expression<Func<T, bool>> where);

        void Update(T item);

        Task UpdateAsync(T item);

        void UpdatePartial(object item);

        Task UpdatePartialAsync(object item);

        void UpdateRange(IEnumerable<T> items);

        Task UpdateRangeAsync(IEnumerable<T> items);

        IQueryable<T> Queryable { get; }

        IQueryable<T> TenantQueryable { get; }

        bool Any();

        bool Any(Expression<Func<T, bool>> where);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> where);

        long Count();

        long Count(Expression<Func<T, bool>> where);

        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<T, bool>> where);

        T Get(object key);

        Task<T> GetAsync(object key);

        IEnumerable<T> List();

        Task<IEnumerable<T>> ListAsync();
    }

}
