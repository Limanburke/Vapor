using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class OrderItem
{
    public int GameId { get; set; }

    public int OrderId { get; set; }
    [Display(Name = "Ціна")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    [Display(Name = "Гра")]
    public virtual Game Game { get; set; } = null!;
    [Display(Name = "Замовлення")]
    public virtual Order Order { get; set; } = null!;
}
