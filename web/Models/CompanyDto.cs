using System.ComponentModel.DataAnnotations;
using web.Db;

namespace web.Models;

public class CompanyDto
{
    [MaxLength(22)]
    [StringLength(22, ErrorMessage = "Идентификатор не может быть больше 22 символов")]
    public string id { get; set; } = Nanoid.Nanoid.Generate();
    public bool deleted { get; set; } = false;

    [MaxLength(255)]
    [Display(Name = "Full Company Name")]
    [Required(ErrorMessage = "required field")]
    public string fullName { get; set; }

    [MaxLength(255)]
    [Display(Name = "Short Company Name")]
    [Required(ErrorMessage = "required field")]
    public string shortName { get; set; }

    [MaxLength(11)]
    [Display(Name = "Phone")]
    [RegularExpression(@"^(\d{11})$", ErrorMessage = "11 digits required")]
    //[StringLength(11, MinimumLength = 11, ErrorMessage = "11 digits required")]
    public string contactPhone { get; set; }

    [MaxLength(255)]
    [Display(Name = "Email")]
    [Required(ErrorMessage = "required field")]
    [RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,4})+$",
     ErrorMessage = "invalid format")]    
    public string contactEmail { get; set; }

    public CompanyDto() { }
    public CompanyDto(Company company)
    {
        id = company.id;
        deleted = company.deleted;
        fullName = company.fullName;
        shortName = company.shortName;
        contactPhone = company.contactPhone;
        contactEmail = company.contactEmail;
    }

    public void Update(Company company)
    {
        company.id = id;
        company.deleted = deleted;
        company.fullName = fullName;
        company.shortName = shortName;
        company.contactPhone = contactPhone;
        company.contactEmail = contactEmail;        
    }

}
