using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace web.Db;


[Table("Company")]
public class Company : BaseTable
{
    [MaxLength(255)]
    [Display(Name ="Full Company Name")]
    public string fullName { get; set; }

    [MaxLength(255)]
    [Display(Name = "Short Company Name")]
    public string shortName { get; set; }

    [MaxLength(255)]
    [Display(Name = "Phone")]
    public string contactPhone { get; set; }

    [MaxLength(255)]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "wrong format")]
    [Required(ErrorMessage = "required field")]
    public string contactEmail { get; set; }

    [MaxLength(1024)]
    public string? address { get; set; }

    [MaxLength(11)]
    public string? phoneForClient { get; set; }


    #region -------------- Навигация -------------------------

    public List<Guest> guests { get; set; }
    public List<User> users { get; set; }
    public List<Review> reviews { get; set; }
    public List<Answer> answers { get; set; }


    #endregion -----------------------------------------------

}

/*
[Table("genderTable")]
[Index(nameof(id), IsUnique = true)]
public class genderTable : BaseTable
{
    [MaxLength(255)]
    public string genderTransc { get; set; }
}
*/






