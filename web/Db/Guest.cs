using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace web.Db;

[Table("Guest")]
[Index(nameof(gkey), IsUnique = true)]
public class Guest : BaseTable
{

    [MaxLength(255)]
    [Display(Name = "Логин")]
    public string login { get; set; }

    [MaxLength(255)]
    public string salt { get; set; }

    [MaxLength(255)]
    public string hash { get; set; }


    /// <summary>
    /// Ключ посетителя для опросов
    /// </summary>
    [MaxLength(22)]
    public string gkey { get; set; } = Nanoid.Nanoid.Generate();

    [MaxLength(255)]
    public string surname { get; set; } = "";

    [MaxLength(255)]
    public string name { get; set; } =  "";

    [MaxLength(255)]
    public string email { get; set; } = "";

    [MaxLength(255)]
    public string phone { get; set; } = "";

    public DateTime dateBirth { get; set; } = new DateTime(2000, 1, 1);

    [MaxLength(22)]
    public string gender { get; set; } = "";



    #region -------------- Навигация -------------------------

    [ForeignKey(nameof(Company))]
    [Required]
    public string companyId { get; set; }
    public Company Company { get; set; }


    public List<Answer> answers { get; set; }

    #endregion -----------------------------------------------
}
