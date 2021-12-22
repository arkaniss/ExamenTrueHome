using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IPropertyRepository Property { get; }
        IStatusRepository Status { get; }
        IActivityRepository Activity { get; }
        void Save();
    }
}
