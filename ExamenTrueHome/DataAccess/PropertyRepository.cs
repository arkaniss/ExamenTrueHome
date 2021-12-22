

using ExamenTrueHome.DataAccess.Repository;
using ExamenTrueHome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamenTrueHome.DataAccess
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public bool ExistInActivity(int id)
        {
            var exist = _db.Activities.Where(e => e.Property_Id == id).Any();
            return exist;
        }

        public IEnumerable<SelectListItem> GetListActive()
        {
            return _db.Properties.Where(e=> e.Status_Id!=3).Select(e => new SelectListItem()
            {
                Text = e.Description,
                Value = e.Id.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetListProperty()
        {
            return _db.Properties.Select(e => new SelectListItem()
            {
                Text = e.Description,
                Value = e.Id.ToString()
            });
        }

        public void Update(Property property)
        {
            var objDb = _db.Properties.FirstOrDefault(e => e.Id == property.Id);
            objDb.Title = property.Title;
            objDb.Description = property.Description;
            objDb.Address = property.Address;
            objDb.Updated_At = DateTime.Now;
            objDb.Status_Id = property.Status_Id;
            objDb.Disabled_At = property.Status_Id==3? DateTime.Now:objDb.Disabled_At;
            _db.SaveChanges();
        }

        
        
    }
}
