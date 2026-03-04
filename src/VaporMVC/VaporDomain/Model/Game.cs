using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class Game : Entity
{
    //public int Id { get; set; }
    [DisplayName("Видавець")]
    public int PublisherId { get; set; }
    [Required(ErrorMessage = "Назва є обов'язковою")]
    [StringLength(200)]
    [DisplayName("Назва")]
    public string Title { get; set; } = null!;

    [DisplayName("Доступна для покупки?")]
    public bool IsAvailable { get; set; }

    [DisplayName("Опис")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Ціна є обов'язковою")]
    //[StringLength(11)]
    [DisplayName("Ціна")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Дата виходу є обов'язковою")]
    [DisplayName("Дата виходу")]
    [DataType(DataType.Date)]
    public DateOnly ReleasedDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
    [Required(ErrorMessage = "Видавець є обов'язковим")]
    [DisplayName("Видавець")]
    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    [Display(Name = "Жанри")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
