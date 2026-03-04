using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class Review : Entity
{
    //public int Id { get; set; }

    public int GameId { get; set; }

    public int UserId { get; set; }
    [Required(ErrorMessage = "Коментар не може бути порожнім")]
    [Display(Name = "Коментар")]
    public string Content { get; set; } = null!;
    [Range(1, 10, ErrorMessage = "Рейтинг повинен бути від 1 до 10")]
    [Display(Name = "Рейтинг")]
    public int Raiting { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Дата створення")]
    public DateOnly CreatedDate { get; set; }
    [Required(ErrorMessage = "Гра є обов'язковою для коментаря")]
    [Display(Name = "Гра")]
    public virtual Game Game { get; set; } = null!;
    [Required(ErrorMessage = "Користувач є обов'язковим для коментаря")]
    [Display(Name = "Користвач")]
    public virtual User User { get; set; } = null!;
}
