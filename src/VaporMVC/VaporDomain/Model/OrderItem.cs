using System;
using System.Collections.Generic;

namespace VaporDomain.Model;

public partial class OrderItem : Entity
{
    public int GameId { get; set; }

    public int OrderId { get; set; }

    public decimal Price { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
