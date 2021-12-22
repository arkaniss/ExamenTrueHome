using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo Requerido")]
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Disabled_At { get; set; }
        [Display(Name ="Status")]
        [ForeignKey("Status")]
        public int Status_Id { get; set; }
        public Status Status { get; set; }
    }
}
