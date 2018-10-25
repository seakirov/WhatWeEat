using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WhatWeEat.Models
{
    public class Eating
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm dd'/'MM'/'yyyy}")]
        public DateTime DateTimeStamp { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "Блюдо")]
        public int? FoodId { get; set; }

        public Food Food { get; set; }
    }
}