using System.Linq.Expressions;
using DotNetCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using InterView.DataBase.Context;
namespace InterView.DataBase.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly InterView.DataBase.Context.Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepository(InterView.DataBase.Context.Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseRepository(ContextFactory context)
        {
            this.context = context;
        }
        private DbSet<T> Set => _context.CommandSet<T>();
        public IQueryable<T> Queryable => _context.QuerySet<T>();
        public IQueryable<T> TenantQueryable => typeof(T).GetProperty("TenantId") != null ? _context.QuerySet<T>().Where("TenantId==" + GetTenantId()) : _context.QuerySet<T>();

        private ContextFactory context;
        private int GetTenantId()
        {
            if (_httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return 0;
            }
            var tenantId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tenant_id");

            if (tenantId != null)
                return int.Parse(tenantId.Value);

            return 0;
        }
        private T SetTenantId(T item)
        {
            if (item.GetType().GetProperty("TenantId") != null && Convert.ToInt32(item.GetType().GetProperty("TenantId").GetValue(item)) == 0)
                //item.GetType().GetProperty("TenantId").SetValue(item, 1);
                item.GetType().GetProperty("TenantId").SetValue(item, GetTenantId());


            if (item.GetType().GetProperty("CreateUserId") != null)
            {
                var individualId = item.GetType().GetProperty("CreateUserId").GetValue(item);
                if ((individualId == null) || (Guid.Parse(individualId.ToString()) == Guid.Empty))
                    item.GetType().GetProperty("CreateUserId").SetValue(item, GetUserId());
            }
            return item;
        }
        private object SetTenantId(object item)
        {
            if (item.GetType().GetProperty("TenantId") != null && Convert.ToInt32(item.GetType().GetProperty("TenantId").GetValue(item)) == 0)
                item.GetType().GetProperty("TenantId").SetValue(item, GetTenantId());

            if (item.GetType().GetProperty("IndividualId") != null)
            {
                var individualId = item.GetType().GetProperty("IndividualId").GetValue(item);
                if (item.GetType().GetProperty("IndividualId") != null && ((individualId == null) || (Guid.Parse(individualId.ToString()) == Guid.Empty)))
                    item.GetType().GetProperty("IndividualId").SetValue(item, GetUserId());

            }

            return item;
        }
        private T SetTenantIdForUpdate(T item)
        {
            if (item.GetType().GetProperty("TenantId") != null && Convert.ToInt32(item.GetType().GetProperty("TenantId").GetValue(item)) == 0)
                item.GetType().GetProperty("TenantId").SetValue(item, GetTenantId());

            if (item.GetType().GetProperty("EditId") != null)
            {
                var createId = item.GetType().GetProperty("EditId").GetValue(item);
                if ((createId == null) || (Guid.Parse(createId.ToString()) == Guid.Empty))
                    item.GetType().GetProperty("EditId").SetValue(item, GetUserId());
            }

            return item;
        }

        private Guid? GetUserId()
        {
            if (_httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return null;
            }
            var individualId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "external_id");

            if (individualId != null)
                return Guid.Parse(individualId.Value);

            return null;
        }
        public void Add(T item)
        {
            item = SetTenantId(item);
            Set.Add(item);

        }

        public virtual Task AddAsync(T item)
        {
            item = SetTenantId(item);
            return Set.AddAsync(item).AsTask();
        }

        public void AddRange(IEnumerable<T> items)
        {
            var list = new List<T>();
            foreach (var item in list)
            {
                var eachitem = SetTenantId(item);
                list.Add(item);
            }
            Set.AddRange(list);
        }

        public virtual Task AddRangeAsync(IEnumerable<T> items)
        {
            var list = new List<T>();
            foreach (var item in items)
            {
                var eachitem = SetTenantId(item);
                list.Add(item);
            }
            return Set.AddRangeAsync(list);
        }

        public bool Any()
        {
            return Queryable.Any();
        }

        public bool Any(Expression<Func<T, bool>> where) => Queryable.Any(where);

        public Task<bool> AnyAsync() => Queryable.AnyAsync();
        public Task<bool> AnyAsync(Expression<Func<T, bool>> where) => Queryable.AnyAsync(where);
        public long Count() => Queryable.LongCount();

        public long Count(Expression<Func<T, bool>> where) => Queryable.LongCount(where);

        public Task<long> CountAsync() => Queryable.LongCountAsync();
        public Task<long> CountAsync(Expression<Func<T, bool>> where) => Queryable.LongCountAsync(where);
        public void Delete(object key)
        {
            var obj = Set.Find(key);
            if (obj == null) return;
            Set.Remove(obj);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            var obj = Set.Where(where);
            if (obj == null) return;
            Set.RemoveRange(obj);
        }

        public virtual Task DeleteAsync(object key)
        {
            return Task.Run(() => Delete(key));
        }

        public Task DeleteAsync(Expression<Func<T, bool>> where) => Task.Run(() => Delete(where));
        public T Get(object key) => _context.DetectChangesLazyLoading(false).Set<T>().Find(key);

        public Task<T> GetAsync(object key) => _context.DetectChangesLazyLoading(false).Set<T>().FindAsync(key).AsTask();

        public IEnumerable<T> List()
        {
            return Queryable.ToList();
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await Queryable.ToListAsync().ConfigureAwait(false);
        }

        public void Update(T item)
        {
            item = SetTenantIdForUpdate(item);

            Set.Update(item);
        }

        public Task UpdateAsync(T item)
        {
            item = SetTenantIdForUpdate(item);

            return Task.Run(() => Update(item));
        }

        public void UpdatePartial(object item)
        {
            item = SetTenantId(item);

            var primaryKeyValues = _context.PrimaryKeyValues<T>(item);

            var entity = Set.Find(primaryKeyValues);

            if (entity is null) return;

            var entry = _context.Entry(entity);

            entry.CurrentValues.SetValues(item);

            foreach (var navigation in entry.Metadata.GetNavigations())
            {
                if (navigation.IsOnDependent || navigation.IsCollection || !navigation.ForeignKey.IsOwnership) continue;

                var property = item.GetType().GetProperty(navigation.Name);

                if (property is null) continue;

                var value = property.GetValue(item, default);

                entry.Reference(navigation.Name).TargetEntry?.CurrentValues.SetValues(value!);
            }
        }

        public Task UpdatePartialAsync(object item) => Task.Run(() => UpdatePartial(item));

        public void UpdateRange(IEnumerable<T> items)
        {
            Set.UpdateRange(items);
        }

        public Task UpdateRangeAsync(IEnumerable<T> items)
        {
            return Task.Run(() => UpdateRange(items));
        }

    }

}
