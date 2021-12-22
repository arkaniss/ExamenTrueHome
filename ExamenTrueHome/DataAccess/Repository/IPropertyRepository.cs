using ExamenTrueHome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess.Repository
{
    public interface IPropertyRepository : IRepository<Property>
    {
        IEnumerable<SelectListItem> GetListProperty();
        IEnumerable<SelectListItem> GetListActive();
        void Update(Property property);

        bool ExistInActivity(int id);
    }
}
