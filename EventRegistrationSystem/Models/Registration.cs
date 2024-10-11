using System.ComponentModel.DataAnnotations;

namespace EventRegistrationSystem.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Participant name is required.")]
        [StringLength(100, ErrorMessage = "Participant name cannot be longer than 100 characters.")]
        public string? ParticipantName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        public Event? Event { get; set; }

        [Required(ErrorMessage = "Event ID is required.")]
        public int EventId { get; set; }

    }
}
