using System.ComponentModel.DataAnnotations;

namespace API;

public class UserDto
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Role { get; set; }
    [Required]
    public required string Token { get; set; }
}
