using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class Status: Entity
{
    //public int Id { get; set; }

    [Display(Name = "Статус")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
