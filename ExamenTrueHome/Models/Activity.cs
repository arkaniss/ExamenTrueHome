using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Property")]
        [Display(Name = "Propiedad")]
        public int Property_Id { get; set; }
        public Property Property { get; set; }
        [Required(ErrorMessage = "campo requerido")]
        public DateTime Schedule { get; set; }
        [Required(ErrorMessage = "campo requerido")]
        public string Title { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        [ForeignKey("Status")]
        [Display(Name ="Status")]
        public int Status_Id { get; set; }
        public Status Status { get; set; }
        [NotMapped]
        public string FormtDateSchedule => Schedule.ToString("G");

        [NotMapped]
        public string FormatDateCreateAt => Created_At.ToString("G");

        [NotMapped]
        public string Condition { get; set; }
    }
}
