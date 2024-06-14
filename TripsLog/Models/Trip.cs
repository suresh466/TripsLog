using System;
using System.ComponentModel.DataAnnotations;

namespace TripsLog.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Destination is required.")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }
        public string Accommodation { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationEmail { get; set; }
        public string ThingToDo1 { get; set; }
        public string ThingToDo2 { get; set; }
        public string ThingToDo3 { get; set; }
    }
}
