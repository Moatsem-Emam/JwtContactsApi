using System.ComponentModel.DataAnnotations;


namespace Shared.Dtos.Contact.Dtos
{
    public class CreateContactDto
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Phone number must be 10 or 11 digits.")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
