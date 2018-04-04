using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IS.Esd.Repository.Infrastructrue.Contract
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Create a new obj to Db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Guid Create(T entity);


        /// <summary>
        /// Remove a entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(Guid id);


        /// <summary>
        /// Update a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(T entity);


        /// <summary>
        /// Preview 
        /// </summary>
        /// <returns></returns>
        T SingleOrDefault(Expression<Func<T, bool>> whereCondition);


        /// <summary>
        /// reurn all rows
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();


        /// <summary>
        /// return all rows of filter condition
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition);


        IUnitOfWork UnitOfWork { get; }
    }
}
