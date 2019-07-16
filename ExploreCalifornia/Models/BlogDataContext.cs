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
    }
}
