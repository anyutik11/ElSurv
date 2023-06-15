/*
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace web.Db;

[Table("Restaurants")]
public class Restaurants : BaseTable
{

    [MaxLength(1024)]
    public string? adress { get; set; }

    [MaxLength(11)]
    public string? phone { get; set; }



    #region -------------- Навигация -------------------------


    [ForeignKey(nameof(Company))]
    [Required]
    public string? restId { get; set; }
    public Company? Restaurant { get; set; }


    [ForeignKey(nameof(Review))]
    [Required]
    public string? reviewId { get; set; }
    public Review? review { get; set; }


    [ForeignKey(nameof(Guest))]
    public string? guestId { get; set; }
    public Guest? guest { get; set; }


    #endregion -----------------------------------------------

}
*/