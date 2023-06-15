using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Db;

[Table("Bonuce")]
[Index(nameof(dtd), nameof(companyId))]
public class Bonuce : BaseTable
{
    public DateTime dtd { get; set; } = DateTime.Now;
    
    public int sum { get; set; } = 0;
        
    [MaxLength(255)]
    public string remark { get; set; } = "";


    #region -------------- Навигация -------------------------

    [ForeignKey(nameof(Company))]
    [Required]
    public string companyId { get; set; }
    public Company Company { get; set; }


    [ForeignKey(nameof(Guest))]
    [Required]
    public string guestId { get; set; }
    public Guest Guest{ get; set; }


    #endregion -----------------------------------------------
}
