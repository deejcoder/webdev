namespace Assignment2.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Assignment2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Assignment2.OSDB.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Assignment2.OSDB.StoreContext context)
        {
            var categories = new List<Category>
            {
                new Category{Name = "Gym and Fitness"},
                new Category{Name = "Footwear"},
                new Category{Name = "Hockey"},
                new Category{Name = "Football" },
                new Category{Name = "Rugby" },
                new Category{Name = "Basketball" },
                new Category{Name = "Tennis" },
                new Category{Name = "Golf" },
                new Category{Name = "Cricket" }

            };

            categories.ForEach(c => context.Categories.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();


            var products = new List<Product>();
            int? cid;

            /*
             * 
             *          HOCKEY
             *          
             */
            cid = categories.Single(c => c.Name == "Hockey").CID;
            products.AddRange(new List<Product>
            {
                new Product { Name = "Adidas V24 Compo5 Hockey Stick", Desc = "Excellent for control has good feedback 375in", Price = 179, CID = cid },
                new Product { Name = "White Hockey Ball", Desc = "Basic tournment ball does the job", Price = 8, CID = cid },
                new Product { Name = "Adidas LX24 Compo1 Hockey Stick", Desc = "Newer model hockey stick which does the job has increased Glass Fibre", Price = 189, CID = cid },
                new Product { Name = "Grays GX4000 Micro Hockey Stick", Desc = "Grays has always been reliable with their hockey sticks and this will not fail you", Price = 389, CID = cid },
                new Product { Name = "TK CB256 Hockey Stick", Desc = "Expensive yet highly effective This hockey stick will increase your control over the ball", Price = 499, CID = cid },
                new Product { Name = "Grays Shinguards White", Desc = "Protect your shins and avoid crying", Price = 19, CID = cid },
                new Product { Name = "Grays Shinguards Black", Desc = "Protect your shins and avoid crying", Price = 19, CID = cid },
                new Product { Name = "Mogo Adult Mouthguard 2 Pack", Desc = "Protect your teeth and assure no fall out", Price = 14, CID = cid }
            });

            /*
             * 
             *          FOOTBALL
             *          
             */

            cid = categories.Single(c => c.Name == "Football").CID;
            products.AddRange(new List<Product>
            {
                new Product { Name = "Adidas Telstar Tourament Ball", Desc = "Be a star with this ball whichll appear in the Russian Football World Cup in 2018", Price = 45, CID = cid },
                new Product { Name = "Adidas Adult Mouthguard", Desc = "Football players still need to protect their teeth from the ball", Price = 15, CID = cid },
                new Product { Name = "Nike Shinguards Medium", Desc = "Protect your shins from hard impact from the ball", Price = 19, CID = cid },
                new Product { Name = "Nike Football Boots", Desc = "Gain better momentum and control the ball better with these football boots", Price = 239, CID = cid }
            });

            /*
             * 
             *          RUGBY
             *          
             */
            cid = categories.Single(c => c.Name == "Rugby").CID;
            products.AddRange(new List<Product>
            {
                new Product { Name = "Adidas Rugby Boots", Desc = "Have grip when in scrums and have more control over the field with these boots", Price = 182, CID = cid },
                new Product { Name = "Nike Rugby Ball, AllStar", Desc = "Professional rugby ball ideal for all types of games casual or professional", Price = 39, CID = cid },
                new Product { Name = "Rugby Kicking Tee", Desc = "Kick well with this tee", Price = 4, CID = cid },
            });

            products.ForEach(c => context.Products.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

        }
    }
}
