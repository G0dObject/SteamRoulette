using System.ComponentModel.DataAnnotations;

public class LoginTransferObject
{
    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(255)]
    public string? Password { get; set; }
}
