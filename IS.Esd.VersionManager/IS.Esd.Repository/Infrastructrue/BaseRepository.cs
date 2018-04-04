using IS.Esd.Domain.Entities;
using IS.Esd.Repository.Infrastructrue.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace IS.Esd.Repository.Infrastructrue
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> _dbSet;

        public BaseRepository(IUnitOfWork UnitOfWork)
        {
            if (UnitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = UnitOfWork;
            _dbSet = Database.Set<T>();
        }

        public virtual Guid Create(T entity)
        {
            var obj = _dbSet.Add(entity);
            Database.SaveChanges();
            return ((Entity)obj).Id;
        }

        public virtual void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                Database.SaveChanges();
            }
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            Database.Entry(entity).State = System.Data.EntityState.Modified;
            Database.SaveChanges();
        }

        public virtual T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = _dbSet.Where(whereCondition).SingleOrDefault();
            return dbResult;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition).AsEnumerable();
        }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        internal DbContext Database { get { return _unitOfWork.Db; } }
    }
}
