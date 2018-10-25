using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WhatWeEat.Models
{
    public class Food
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [Remote("IsFoodNameExist", "Food", ErrorMessage = "Это блюдо уже кто-то когда-то ел")]
        public string Name { get; set; }

        public virtual ICollection<Eating> Eatings { get; set; }

        public Food()
        {
            Eatings = new List<Eating>();
        }
    }
}