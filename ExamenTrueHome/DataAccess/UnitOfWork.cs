using ExamenTrueHome.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Property = new PropertyRepository(_db);
            Status = new StatusRepository(_db);
            Activity = new ActivityRepository(_db);
        }

        public IPropertyRepository Property { get; private set; }
        public IStatusRepository Status { get; private set; }
        public IActivityRepository Activity { get; private set; }

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
