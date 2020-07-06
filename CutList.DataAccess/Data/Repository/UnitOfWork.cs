using CutList.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutList.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //database access
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Job = new JobRepository(_db);
        }

        public IJobRepository Job { get; private set; }         //can only be set here

        //release the allocated resources for this context
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
