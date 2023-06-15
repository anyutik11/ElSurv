using System.ComponentModel.DataAnnotations;

namespace web.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(5, ErrorMessage = "Min length is 5 symbols")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Password is required")]    
    public string? Password { get; set; }

    public bool loginFailed { get; set; }

}
