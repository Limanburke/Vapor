using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class Order : Entity
{
    //public int Id { get; set; }

    [Display(Name = "Користувач")]
    public int UserId { get; set; }

    [Display(Name = "Статус")]
    public int StatusId { get; set; }
    [Display(Name = "Дата створення замовлення")]
    [Required(ErrorMessage = "Дата створення замовлення є обов'язковою")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [Display(Name = "Статус")]
    public virtual Status Status { get; set; } = null!;
    [Display(Name = "Користувач")]
    public virtual User User { get; set; } = null!;
}
