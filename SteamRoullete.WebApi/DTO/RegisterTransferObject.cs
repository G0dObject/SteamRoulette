using System.ComponentModel.DataAnnotations;

public class RegisterTransferObject
{
    [Required]
    [MinLength(2)]
    [MaxLength(14)]
    public string? Username { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string? Password { get; set; }
}
