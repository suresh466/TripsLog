using System.ComponentModel.DataAnnotations;
using System;

namespace TripsLog.ViewModels
{
    public class TripDetailsViewModel
    {
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Accommodation { get; set; }
    }
}
