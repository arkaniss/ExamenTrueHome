using ExamenTrueHome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess.Repository
{
    public interface IStatusRepository : IRepository<Status>
    {
        IEnumerable<SelectListItem> GetListItems();
        void Update(Status status);
    }
}
