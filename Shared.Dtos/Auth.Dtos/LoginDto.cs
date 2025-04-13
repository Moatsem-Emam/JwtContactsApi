using System.ComponentModel.DataAnnotations;


namespace Shared.Dtos.Auth.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
