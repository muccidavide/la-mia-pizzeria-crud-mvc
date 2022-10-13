using la_mia_pizzeria_crud_mvc;
using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public PizzaContext()
        {
        }

        public PizzaContext(DbContextOptions<PizzaContext> options)
                : base(options)
            {
            }
      

        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ex-pizza-store;Integrated Security=True");
        }
    }
}
