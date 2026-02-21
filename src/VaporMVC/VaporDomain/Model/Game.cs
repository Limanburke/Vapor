using System;
using System.Collections.Generic;

namespace VaporDomain.Model;

public partial class Game : Entity
{
    //public int Id { get; set; }

    public int PublisherId { get; set; }

    public string Title { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleasedDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
