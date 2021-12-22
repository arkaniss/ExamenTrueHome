using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.Models.ViewModel
{
    public class ActivityViewModel
    {
        public Activity Activity { get; set; }
        public IEnumerable<SelectListItem> Properties { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
