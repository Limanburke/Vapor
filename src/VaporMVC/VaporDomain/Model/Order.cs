using System;
using System.Collections.Generic;

namespace VaporDomain.Model;

public partial class Order : Entity
{
    //public int Id { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
