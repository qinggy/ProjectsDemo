using IS.Esd.Domain;
using IS.Esd.Repository.Infrastructrue.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace IS.Esd.Repository.Infrastructrue
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _Transaction;
        private readonly VersionDbContext _Db;

        public UnitOfWork()
        {
            _Db = new VersionDbContext();
        }

        public void Commit()
        {
            _Db.SaveChanges();
            _Transaction.Complete();
        }

        public void StartTransaction()
        {
            _Transaction = new TransactionScope();
        }

        public Domain.VersionDbContext Db
        {
            get { return _Db; }
        }

        public void Dispose()
        {
            _Db.Dispose();
        }
    }
}
