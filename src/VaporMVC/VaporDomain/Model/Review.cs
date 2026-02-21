using System;
using System.Collections.Generic;

namespace VaporDomain.Model;

public partial class Review : Entity
{
    //public int Id { get; set; }

    public int GameId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public int Raiting { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
