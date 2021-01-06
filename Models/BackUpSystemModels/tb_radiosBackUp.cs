using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImcLabApp.Models.BackUpSystemModels
{
    public class tb_radiosBackUp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string testName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string urlName { get; set; }
        [Required]
        public string Datetime { get; set; }
        [Required]
        public string dateWasAdded { get; set; }
        [Required]
        public string PatientMedicalNumber { get; set; }
        [Required]
        public string PatientName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int patients_Id { get; set; }
    }
}