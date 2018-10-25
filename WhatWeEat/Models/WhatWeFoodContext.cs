using System.Data.Entity;

namespace WhatWeEat.Models
{
    public class WhatWeEatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Eating> Eatings { get; set; }
    }
}