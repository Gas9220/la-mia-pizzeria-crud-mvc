using la_mia_pizzeria_crud_mvc.CustomLoggers;
using la_mia_pizzeria_crud_mvc.Database;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        private ICustomLogger _myLogger;

        public PizzaController(ICustomLogger _logger)
        {
            _myLogger = _logger;
        }

        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                _myLogger.WriteLog("Admin visit index page", "READ");

                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("Index", pizzas);
            }
        }

        public IActionResult Details(int id)
        {
            _myLogger.WriteLog($"Admin visit details page for {id}", "READ");

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
            _myLogger.WriteLog("Admin create new pizza", "CREATE");

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
            _myLogger.WriteLog("Admin visit create new pizza page", "CREATE");
            return View("Create");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            _myLogger.WriteLog("Admin visit edit new pizza page", "EDIT");

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
            _myLogger.WriteLog($"Admin edit pizza with {id}", "EDIT");

            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            using (PizzaContext context = new PizzaContext())
            {
                Pizza? pizzaToEdit = context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _myLogger.WriteLog($"Admin delete pizza with {id}", "DELETE");

            using (PizzaContext context = new PizzaContext())
            {
                Pizza? pizzaToDelete = context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    context.Pizzas.Remove(pizzaToDelete);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
