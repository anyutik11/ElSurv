using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Db;

public class BaseTable : ICloneable
{
    [Key]
    [Column(TypeName = "char")]
    [MaxLength(22)]
    [StringLength(22, ErrorMessage = "Идентификатор не может быть больше 22 символов")]
    public virtual string id { get; set; } = Nanoid.Nanoid.Generate();
    public bool deleted { get; set; } = false;


    public object Clone()
    {
        return this.MemberwiseClone();
    }

    /*
    /// <summary>
    /// Utc
    /// </summary>
    public DateTime? created { get; set; }

    /// <summary>
    /// Utc
    /// </summary>
    [Timestamp]
    public DateTime? modified { get; set; }

    public DateTime? deleted { get; set; }


    /// <summary>
    /// Utc
    /// </summary>
    [Timestamp]
    public byte[] rowversion { get; set; }
    */

}
