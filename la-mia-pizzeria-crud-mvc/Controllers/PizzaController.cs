using la_mia_pizzeria_crud_mvc.Database;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("Index", pizzas);
            }
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? foundedPizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (foundedPizza == null)
                {
                    return NotFound($"Nessuna pizza trovata con l'id {id} ");
                }
                else
                {
                    return View("Details", foundedPizza);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }
            using (PizzaContext context = new PizzaContext())
            {
                Pizza newPizza = new Pizza();

                newPizza.Name = data.Name;
                newPizza.Description = data.Description;
                newPizza.Price = data.Price;
                newPizza.PhotoUrl = data.PhotoUrl;

                context.Pizzas.Add(newPizza);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

    }
}
