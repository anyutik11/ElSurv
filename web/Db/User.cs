using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Db;

[Table("User")]
public class User : BaseTable
{    
    [MaxLength(255)]
    [Display(Name = "Логин")]
    public string login { get; set; }

    [MaxLength(255)]
    public string salt { get; set; }

    [MaxLength(255)]
    public string hash { get; set; }


    [MaxLength(255)]
    [Display(Name = "Отчество")]
    public string? surname { get; set; }

    [MaxLength(255)]
    [Display(Name = "Имя")]
    public string name { get; set; }

    [MaxLength(255)]
    [Display(Name = "Email")]
    public string contactEmail { get; set; }

    [MaxLength(255)]
    [Display(Name = "Контактный телефон")]
    public string contactPhone { get; set; }

    [MaxLength(255)]
    [Display(Name = "Дата рождения")]
    public DateTime? dateBirth { get; set; }

    [MaxLength(22)]
    [Display(Name = "Пол")]
    public string gender { get; set; }




    #region -------------- Навигация -------------------------

    [ForeignKey(nameof(Company))]
    [Required]
    public string companyId { get; set; }
    public Company Company { get; set; }

    #endregion -----------------------------------------------



}
