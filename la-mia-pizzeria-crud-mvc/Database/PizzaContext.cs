using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_mvc.Database
{
    public class PizzaContext: DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzaDb;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
