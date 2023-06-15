using System.ComponentModel.DataAnnotations;

namespace web.Models;

public class ReviewModel
{
    [Required(ErrorMessage = "Key is required")]
    [MinLength(5, ErrorMessage = "Min length is 5 symbols")]
    public string? key { get; set; }

}

