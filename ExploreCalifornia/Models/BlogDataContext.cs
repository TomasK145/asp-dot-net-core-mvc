using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class BlogDataContext : DbContext
    {
        //kedze v startup je definovane pouzitie "AddDbContext", treba do konstruktora vramci DbContext triedy pridat options
        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
        {
            //Database.EnsureCreated(); //overi ci DB existuje, ak neexistuje, EF vytvori SQL schemu pre vytvorenie DB
        }

        public DbSet<Post> Posts { get; set; } //reprezentuje tabulku v DB ako strongly-typed object

        public IQueryable<MonthlySpecial> MonthlySpecials
        {
            get
            {
                return new[]
                {
                    new MonthlySpecial {
                        Key = "calm",
                        Name = "California Calm Package",
                        Type = "Day Spa Package",
                        Price = 250,
                    },
                    new MonthlySpecial {
                        Key = "desert",
                        Name = "From Desert to Sea",
                        Type = "2 Day Salton Sea",
                        Price = 350,
                    },
                    new MonthlySpecial {
                        Key = "backpack",
                        Name = "Backpack Cali",
                        Type = "Big Sur Retreat",
                        Price = 620,
                    },
                    new MonthlySpecial {
                        Key = "taste",
                        Name = "Taste of California",
                        Type = "Tapas & Groves",
                        Price = 150,
                    },
                }.AsQueryable();
            }
        }

    }
}
