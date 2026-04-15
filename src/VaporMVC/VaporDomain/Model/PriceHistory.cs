using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporDomain.Model;

public partial class PriceHistory : Entity
{
    public int GameId { get; set; }

    [Display(Name = "Стара ціна")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal OldPrice { get; set; }

    [Display(Name = "Нова ціна")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal NewPrice { get; set; }

    [Display(Name = "Дата зміни")]
    [DataType(DataType.DateTime)]
    public DateTime ChangedData { get; set; }

    [Display(Name = "Гра")]
    public virtual Game Game { get; set; } = null!;
}
