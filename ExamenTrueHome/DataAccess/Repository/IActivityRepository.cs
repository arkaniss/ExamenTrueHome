
using ExamenTrueHome.Models;
using ExamenTrueHome.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess.Repository
{
    public interface IActivityRepository : IRepository<Activity>
    {
        void Update(Activity activity);
        bool CheckIfExis(ActivityViewModel activity);
        void UpdateCancel(Activity activity);
        IEnumerable<Activity> GetAllCondition(string includeProperties = null);

        IEnumerable<Activity> GetAllFilterCondition(string includeProperties = null);
        IEnumerable<Activity> GetAllFilterData(string start = null, string end = null, int status = 0,string includeProperties = null);


    }
}
