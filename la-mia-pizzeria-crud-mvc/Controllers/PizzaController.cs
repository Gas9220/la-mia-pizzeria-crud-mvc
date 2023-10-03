using la_mia_pizzeria_crud_mvc.Database;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza? pizzaToEdit = context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(pizzaToEdit);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            using (PizzaContext context = new PizzaContext())
            {
                Pizza? pizzaToEdit = context.Pizzas.Where(pizza => pizza.Id != id).FirstOrDefault();

                if (pizzaToEdit != null)
                {
                    pizzaToEdit.Name = data.Name;
                    pizzaToEdit.Description = data.Description;
                    pizzaToEdit.Price = data.Price;
                    pizzaToEdit.PhotoUrl = data.PhotoUrl;

                    context.SaveChanges();

                    return RedirectToAction("Details", "Pizza", new { id = pizzaToEdit.Id });
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
