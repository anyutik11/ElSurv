using System.ComponentModel.DataAnnotations;
using web.Db;

namespace web.Models;

public class ReviewDto
{
    public string? id { get; set; }
    public string? companyName { get; set; }
    public string? companyId { get; set; }

    [Display(Name = "State")]
    public bool? active { get; set; }
    public string? description { get; set; }
    public string? key { get; set; }
    public string? question1 { get; set; }
    public string? question2 { get; set; }
    public string? question3 { get; set; }

    public ReviewDto()
    {        
    }

    public ReviewDto(Review review, string compName)
    {
        id = review.id;
        companyName = compName;
        companyId = review.companyId;
        active = review.active;
        description = review.description;
        key = review.key;
        question1 = review.question1;
        question2 = review.question2;
        question3 = review.question3;
    }
}

