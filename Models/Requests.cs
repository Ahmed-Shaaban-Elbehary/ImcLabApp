using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImcLabApp.Models
{
    public class Requests
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string medicalNumber { get; set; }
        [Required]
        public string nationalId { get; set; }
        [Required]
        public string dateTime { get; set; }

        public bool lb_Request { get; set; }
        public bool rd_Request { get; set; }
        public bool tr_Request { get; set; }

    }
}