using System.ComponentModel.DataAnnotations;


namespace Shared.Dtos.Auth.Dtos
{
    public class RegistrationDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email like : email@example.com")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

}
