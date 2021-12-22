using ExamenTrueHome.DataAccess.Repository;
using ExamenTrueHome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    {
        private readonly ApplicationDbContext _db;
        public StatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListItems()
        {
            return _db.Status.Select(e => new SelectListItem()
            {
                Text = e.Description,
                Value = e.Id.ToString()
            });
        }

        public void Update(Status status)
        {
            var objDb = _db.Properties.FirstOrDefault(e => e.Id == status.Id);
            objDb.Description = status.Description;
            _db.SaveChanges();
        }
    }
}
