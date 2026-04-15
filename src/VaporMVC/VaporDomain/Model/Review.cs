using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class Review : Entity
{
    [Display(Name = "Гра")]
    public int GameId { get; set; }
    [Display(Name = "Користувач")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Коментар не може бути порожнім")]
    [Display(Name = "Коментар")]
    public string Content { get; set; } = null!;
    [Range(1, 10, ErrorMessage = "Рейтинг повинен бути від 1 до 10")]
    [Display(Name = "Рейтинг")]
    public int Rating { get; set; }
    [DataType(DataType.DateTime)]
    [Display(Name = "Дата створення")]
    public DateTime CreatedDate { get; set; }
    [Required(ErrorMessage = "Гра є обов'язковою для коментаря")]
    [Display(Name = "Гра")]
    public virtual Game Game { get; set; } = null!;
    [Required(ErrorMessage = "Користувач є обов'язковим для коментаря")]
    [Display(Name = "Користвач")]
    public virtual User User { get; set; } = null!;
}
