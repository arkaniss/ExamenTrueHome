using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Activity")]
        public int Activity_Id { get; set; }
        public Activity Activity { get; set; }
        public string Answers { get; set; }
        public DateTime Created_At { get; set; }
    }
}
