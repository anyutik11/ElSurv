using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Db;

[Table("Review")]
public class Review : BaseTable
{
    [Display(Name = "Включен")]
    public bool active { get; set; } = false;

    [MaxLength(22, ErrorMessage = "Длина ключа не должна превышать 22 символа")]
    [Display(Name = "Ключ")]
    [Required(ErrorMessage = "Необходимо задать ключ")]    
    public string key { get; set; }

    [MaxLength(1024)]
    [Display(Name = "Описание")]
    public string description { get; set; }

    [MaxLength(1024)]
    [Display(Name = "Вопрос 1")]
    public string question1 { get; set; }

    [MaxLength(1024)]
    [Display(Name = "Вопрос 2")]
    public string question2 { get; set; }

    [MaxLength(1024)]
    [Display(Name = "Вопрос 3")]
    public string question3 { get; set; }




    #region -------------- Навигация -------------------------

    [ForeignKey(nameof(Company))]
    [Required]
    public string companyId { get; set; }
    public Company Company { get; set; }


    public List<Answer> answers { get; set; }    
    public List<Guest> guests { get; set; }
    

    #endregion -----------------------------------------------

}
