using System;
using System.Collections.Generic;

namespace VaporDomain.Model;

public partial class Status: Entity
{
    //public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
