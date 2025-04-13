using System.ComponentModel.DataAnnotations;


namespace Shared.Dtos.Auth.Dtos
{
    public class AddRoleDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Role { get; set; }
    }
}