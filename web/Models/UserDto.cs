using System.ComponentModel.DataAnnotations;
using web.Db;

namespace web.Models;

public class UserDto
{
    [MaxLength(22)]
    [StringLength(22, ErrorMessage = "Идентификатор не может быть больше 22 символов")]
    public string id { get; set; } = Nanoid.Nanoid.Generate();
    public bool deleted { get; set; } = false;

    [MaxLength(255)]
    [Display(Name = "Логин")]
    public string login { get; set; }

    
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

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Дата рождения")]
    public DateTime? dateBirth { get; set; }

    [MaxLength(22)]
    [Display(Name = "Пол")]
    public string gender { get; set; }

    [MaxLength(255)]
    [Display(Name = "Password")]
    [StringLength(255, MinimumLength = 4, ErrorMessage = "Password must be less 255 symbolds and more then 4 ones")]
    public string password { get; set; }

    public UserDto()
    {        
    }

    public UserDto(User user)
    {
        id = user.id;
        deleted = user.deleted;
        login = user.login;
        name = user.name;
        surname = user.surname;
        contactEmail = user.contactEmail;
        contactPhone = user.contactPhone;
        dateBirth = user.dateBirth;
        gender = user.gender;
    }

    public void Update(User user)
    {
        user.id = id;
        user.deleted = deleted;
        user.login = login;
        user.name = name;
        user.surname = surname;
        user.contactEmail = contactEmail;
        user.contactPhone = contactPhone;
        user.dateBirth = dateBirth;
        user.gender = gender;
    }
}
