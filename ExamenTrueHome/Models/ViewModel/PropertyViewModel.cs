using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.Models
{
    public class PropertyViewModel
    {
        public Property Property { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
