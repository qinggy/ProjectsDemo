using IS.Esd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Repository.Infrastructrue.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call this to Commit unit of work
        /// </summary>
        void Commit();

        /// <summary>
        /// Starts a Transaction on this unit of work
        /// </summary>
        void StartTransaction();

        /// <summary>
        /// Return the DataBase reference for this unit of work
        /// </summary>
        VersionDbContext Db { get; }
    }
}
