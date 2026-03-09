using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class Genre : Entity
{
    //public int Id { get; set; }
    [Required(ErrorMessage = "Назва є обов'язковою")]
    [StringLength(100)]
    //[RegularExpression(@"^[a-zA-Zа-яА-ЯіІєЄґҐ0-9\s]+$", ErrorMessage = "Допустимі лише літери та цифри")]
    [Display(Name = "Жанр")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
