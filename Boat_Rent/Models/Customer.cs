using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Boat_Rent.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int BoatId { get; set; }

        [Required(ErrorMessage = "Boat No. is required.")]
        public string BoatNumber { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }
    }
}