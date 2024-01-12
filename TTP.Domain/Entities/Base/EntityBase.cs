using System.ComponentModel.DataAnnotations.Schema;

namespace TTP.Domain.Entities.Base;

public class EntityBase
{
    [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
}