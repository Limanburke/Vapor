using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace VaporDomain.Model;

public partial class Publisher : Entity
{
    //public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [StringLength(100)]
    [Display(Name = "Видавець")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
