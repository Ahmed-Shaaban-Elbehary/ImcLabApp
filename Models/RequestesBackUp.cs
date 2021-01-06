using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImcLabApp.Models
{
    public class RequestesBackUp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string medicalNumber { get; set; }
        [Required]
        public string nationalId { get; set; }
        [Required]
        public string dateTime { get; set; }
        [Required]
        public string RemovedDate { get; set; }
        [Required]
        public string UserName { get; set; }
        public bool lb_Request { get; set; }
        public bool rd_Request { get; set; }
        public bool tr_Request { get; set; }

    }
}