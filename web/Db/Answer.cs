using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Db;

[Table("Answer")]
public class Answer : BaseTable
{
    public DateTime date { get; set; }
    public int? mark { get; set; }

    [MaxLength(1024)]
    public string? text1 { get; set; }

    [MaxLength(1024)]
    public string? text2 { get; set; }

    [MaxLength(1024)]
    public string? text3 { get; set; }

    [MaxLength(11)]
    public string? phone { get; set; }



    #region -------------- Навигация -------------------------


    [ForeignKey(nameof(Company))]
    [Required]
    public string companyId { get; set; }
    public Company Company { get; set; }


    [ForeignKey(nameof(Review))]
    [Required]
    public string reviewId { get; set; }
    public Review review { get; set; }


    [ForeignKey(nameof(Guest))]
    public string? guestId { get; set; }
    public Guest? guest { get; set; }


    #endregion -----------------------------------------------

}
