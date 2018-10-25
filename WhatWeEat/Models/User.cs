using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WhatWeEat.Models
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public virtual ICollection<Eating> Eatings { get; set; }

        public User()
        {
            Eatings = new List<Eating>();
        }
    }
}