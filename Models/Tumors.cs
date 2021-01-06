using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImcLabApp.Models
{
    public class Tumors
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="من فضلك أدخل أسم التقرير")]
        public string testName { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string urlName { get; set; }
        [Required]
        public string Datetime { get; set; }

        [Required]
        [ForeignKey("Patients")]
        public int patients_Id { get; set; }
        public virtual Patients Patients { get; set; }
    }
}