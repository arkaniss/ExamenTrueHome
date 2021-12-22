using ExamenTrueHome.DataAccess.Repository;
using ExamenTrueHome.Models;
using ExamenTrueHome.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        private readonly ApplicationDbContext _db;
        public ActivityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool CheckIfExis(ActivityViewModel activity)
        {
            var objDb = false;
            if (activity.Activity.Id > 0)
            {
                objDb = _db.Activities
                    .Where(e => ((activity.Activity.Schedule >= e.Schedule && activity.Activity.Schedule <= e.Schedule.AddHours(1))
                     || activity.Activity.Schedule.AddHours(1) >= e.Schedule && 
                            activity.Activity.Schedule.AddHours(1) <= e.Schedule.AddHours(1))
                     && e.Property_Id == activity.Activity.Property_Id && e.Id != activity.Activity.Id)
                    .Any();
            }

            else
            {
                objDb = _db.Activities
                    .Where(e => ((activity.Activity.Schedule >= e.Schedule && activity.Activity.Schedule <= e.Schedule.AddHours(1))
                     || activity.Activity.Schedule.AddHours(1) >= e.Schedule &&
                            activity.Activity.Schedule.AddHours(1) <= e.Schedule.AddHours(1))
                    && e.Property_Id == activity.Activity.Property_Id)
                .Any();
            }
            

            return objDb;
        }

        public IEnumerable<Activity> GetAllCondition(string includeProperties = null)
        {
            var objDb = GetAll(filter: e => e.Schedule >= DateTime.Now.AddDays(-3) && e.Schedule <= DateTime.Now.AddDays(14), includeProperties: includeProperties).ToList(); ;
            objDb.ForEach(e =>
            {
                e.Condition = e.Schedule < DateTime.Now ? "Atrasada" : "Pendiende a Realizar";
                e.Condition = e.Status_Id == 2 ? "Finalizada" : e.Condition;
            });

            return objDb;
        }

        public IEnumerable<Activity> GetAllFilterCondition(string includeProperties = null)
        {
            var objDb = GetAll(includeProperties: includeProperties).ToList(); ;
            objDb.ForEach(e =>
            {
                e.Condition = e.Schedule < DateTime.Now ? "Atrasada" : "Pendiende a Realizar";
                e.Condition = e.Status_Id == 2 ? "Finalizada" : e.Condition;
            });

            return objDb;
        }

        public IEnumerable<Activity> GetAllFilterData(string start = null, string end = null, int status = 0, string includeProperties = null)
        {
            var _start = DateTime.Parse(start);
            var _end = DateTime.Parse(end);
            var objDb = status == 0 ? GetAll(filter: e => e.Schedule >= _start && e.Schedule <= _end, includeProperties: "Property,Status").ToList() :
                GetAll(filter: e => e.Schedule >= _start && e.Schedule <= _end && e.Status_Id == status, includeProperties: "Property,Status").ToList();
            objDb.ForEach(e =>
            {
                e.Condition = e.Schedule < DateTime.Now ? "Atrasada" : "Pendiende a Realizar";
                e.Condition = e.Status_Id == 2 ? "Finalizada" : e.Condition;
            });
            return objDb;
        }

        public void Update(Activity activity)
        {
            var objDb = _db.Activities.FirstOrDefault(e => e.Id == activity.Id);
            objDb.Schedule = activity.Schedule;
            objDb.Update_At = activity.Update_At;
            
            _db.SaveChanges();
        }

        public void UpdateCancel(Activity activity)
        {
            var objDb = _db.Activities.FirstOrDefault(e => e.Id == activity.Id);
            objDb.Update_At = activity.Update_At;
            objDb.Status_Id = activity.Status_Id;
            _db.SaveChanges();
        }
    }
}
