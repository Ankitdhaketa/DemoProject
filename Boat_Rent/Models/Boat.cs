using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Boat_Rent.Models
{
    public class Boat
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Boat name is required.")]
        public string Name { get; set; }

       
        public string Image { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        public HttpPostedFileBase Postedfile { get; set; }

        [Required(ErrorMessage = "HourlyRate is required.")]
        public int Rate { get; set; }
        public int IsRent { get; set; }
    }
}