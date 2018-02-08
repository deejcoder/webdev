﻿using Assignment2.Models;
using System.Data.Entity;

namespace Assignment2.OSDB
{
    public class StoreContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}