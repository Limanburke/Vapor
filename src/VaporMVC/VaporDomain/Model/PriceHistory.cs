using System;
using System.Collections.Generic;

namespace VaporDomain.Model;

public partial class PriceHistory : Entity
{
    //public int Id { get; set; }

    public int GameId { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public DateOnly ChangedData { get; set; }

    public virtual Game Game { get; set; } = null!;
}
