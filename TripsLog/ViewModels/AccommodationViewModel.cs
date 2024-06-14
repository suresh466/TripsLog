using System.ComponentModel.DataAnnotations;

namespace TripsLog.ViewModels
{
    public class AccommodationViewModel
    {
        public string Accommodation { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string AccommodationPhone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string AccommodationEmail { get; set; }
    }

}
