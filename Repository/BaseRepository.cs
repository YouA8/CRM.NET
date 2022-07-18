using IRepository;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly CRMDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(CRMDbContext dbcontext)
        {
            _dbContext = dbcontext;
            _dbSet = _dbContext.Set<T>();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                _dbContext.Database.BeginTransaction(isolationLevel);
            }
        }

        public void BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                _dbContext.Database.BeginTransactionAsync(isolationLevel);
            }
        }

        public void Commit()
        {
            var transaction = _dbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void CommitAsync()
        {
            var transaction = _dbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public void Rollback()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.CurrentTransaction.Rollback();
            }
        }

        public void RollbackAsync()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.CurrentTransaction.RollbackAsync();
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public CRMDbContext DbContext
        {
            get { return _dbContext; }
        }

        public IQueryable<T> Entities
        {
            get { return _dbSet.AsNoTracking(); }
        }

        public IQueryable<T> TrackEntities
        {
            get { return _dbSet; }
        }

        public T Add(T entity, bool isSave = true)
        {
            _dbSet.Add(entity);
            if (isSave)
            {
                SaveChanges();
            }
            return entity;
        }

        public async Task<T> AddAsync(T entity, bool isSave = true)
        {
            await _dbSet.AddAsync(entity);
            if (isSave)
            {
                await SaveChangesAsync();
            }
            return entity;
        }

        public void AddRange(IEnumerable<T> entitys, bool isSave = true)
        {
            _dbSet.AddRange(entitys);
            if (isSave)
            {
                SaveChanges();
            }
        }

        public async void AddRangeAsync(IEnumerable<T> entitys, bool isSave = true)
        {
            await _dbSet.AddRangeAsync(entitys);
            if (isSave)
            {
                await SaveChangesAsync();
            }
        }

        public void Delete(T entity, bool isSave = true)
        {
            _dbSet.Remove(entity);
            if (isSave)
            {
                SaveChanges();
            }
        }

        public void Delete(bool isSave = true, params T[] entitys)
        {
            _dbSet.RemoveRange(entitys);
            if (isSave)
            {
                SaveChanges();
            }
        }

        public void Delete(object id, bool isSave = true)
        {
            _dbSet.Remove(_dbSet.Find(id));
            if (isSave)
            {
                SaveChanges();
            }
        }

        public void Delete(Expression<Func<T, bool>> @where, bool isSave = true)
        {
            T[] entitys = _dbSet.Where<T>(@where).ToArray();
            if (entitys.Length > 0)
            {
                _dbSet.RemoveRange(entitys);
            }
            if (isSave)
            {
                SaveChanges();
            }
        }

        public void Update(T entity, bool isSave = true)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            if (isSave)
            {
                SaveChanges();
            }
        }

        public void Update(bool isSave = true, params T[] entitys)
        {
            var entry = _dbContext.Entry(entitys);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            if (isSave)
            {
                SaveChanges();
            }
        }

        public bool Any(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().Any(@where);
        }

        public int Count()
        {
            return _dbSet.AsNoTracking().Count();
        }

        public int Count(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().Count(@where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(@where);
        }

        public T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return _dbSet.AsNoTracking().OrderByDescending(order).FirstOrDefault(@where);
            }
            else
            {
                return _dbSet.AsNoTracking().OrderBy(order).FirstOrDefault(@where);
            }
        }

        public IQueryable<T> Distinct(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().Where(@where).Distinct();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> @where)
        {
            return _dbSet.Where(@where);
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return _dbSet.Where(@where).OrderByDescending(order);
            }
            else
            {
                return _dbSet.Where(@where).OrderBy(order);
            }
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            count = Count();
            if (isDesc)
            {
                return _dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return _dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return _dbSet.AsNoTracking().OrderByDescending(order);
            }
            else
            {
                return _dbSet.AsNoTracking().OrderBy(order);
            }
        }

        public T GetById<Ttype>(Ttype id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync<Ttype>(Ttype id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Ttype Max<Ttype>(Expression<Func<T, Ttype>> column)
        {
            if (_dbSet.AsNoTracking().Any())
            {
                return _dbSet.AsNoTracking().Max(column);
            }
            return default(Ttype);
        }

        public Ttype Max<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where)
        {
            if (_dbSet.AsNoTracking().Any(@where))
            {
                return _dbSet.AsNoTracking().Where(@where).Max(column);
            }
            return default(Ttype);
        }

        public Ttype Min<Ttype>(Expression<Func<T, Ttype>> column)
        {
            if (_dbSet.AsNoTracking().Any())
            {
                return _dbSet.AsNoTracking().Min<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public Ttype Min<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where)
        {
            if (_dbSet.AsNoTracking().Any(@where))
            {
                return _dbSet.AsNoTracking().Where(@where).Min<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public Ttype Sum<Ttype>(Expression<Func<T, Ttype>> selector, Expression<Func<T, bool>> @where) where Ttype : new()
        {
            object result = 0;

            if (new Ttype().GetType() == typeof(decimal))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, decimal>>);
            }
            if (new Ttype().GetType() == typeof(decimal?))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, decimal?>>);
            }
            if (new Ttype().GetType() == typeof(double))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, double>>);
            }
            if (new Ttype().GetType() == typeof(double?))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, double?>>);
            }
            if (new Ttype().GetType() == typeof(float))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, float>>);
            }
            if (new Ttype().GetType() == typeof(float?))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, float?>>);
            }
            if (new Ttype().GetType() == typeof(int))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, int>>);
            }
            if (new Ttype().GetType() == typeof(int?))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, int?>>);
            }
            if (new Ttype().GetType() == typeof(long))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, long>>);
            }
            if (new Ttype().GetType() == typeof(long?))
            {
                result = _dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, long?>>);
            }
            return (Ttype)result;
        }
    }
}
