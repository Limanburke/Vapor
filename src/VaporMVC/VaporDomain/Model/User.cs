using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporDomain.Model;

public partial class User : Entity
{
    //public int Id { get; set; }

    [Required(ErrorMessage = "Ім'я користувача є обов'язковим")]
    [StringLength(50)]
    [Display(Name = "Ім'я користувача")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Пошта є обов'язковою")]
    [Display(Name = "Пошта")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
