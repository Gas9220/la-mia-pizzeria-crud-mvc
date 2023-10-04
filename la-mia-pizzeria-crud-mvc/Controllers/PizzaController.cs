using la_mia_pizzeria_crud_mvc.CustomLoggers;
using la_mia_pizzeria_crud_mvc.Database;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public PizzaController(ICustomLogger _logger, PizzaContext myDatabase)
        {
            _myLogger = _logger;
            _myDatabase = myDatabase;
        }

        public IActionResult Index()
        {
            _myLogger.WriteLog("Admin visit index page", "READ");

            List<Pizza> pizzas = _myDatabase.Pizzas.ToList<Pizza>();

            return View("Index", pizzas);
        }

        public IActionResult Details(int id)
        {
            _myLogger.WriteLog($"Admin visit details page for {id}", "READ");

            Pizza? foundedPizza = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (foundedPizza == null)
            {
                return NotFound($"Nessuna pizza trovata con l'id {id} ");
            }
            else
            {
                return View("Details", foundedPizza);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            _myLogger.WriteLog("Admin create new pizza", "CREATE");

            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;
                return View("Create", data);
            }
            Pizza newPizza = new Pizza();

            newPizza.Name = data.Pizza.Name;
            newPizza.Description = data.Pizza.Description;
            newPizza.Price = data.Pizza.Price;
            newPizza.PhotoUrl = data.Pizza.PhotoUrl;

            newPizza.CategoryId = data.Pizza.CategoryId;

            _myDatabase.Pizzas.Add(newPizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            _myLogger.WriteLog("Admin visit create new pizza page", "CREATE");

            List<Category> categories = _myDatabase.Categories.ToList();

            PizzaFormModel model = new PizzaFormModel();
            model.Pizza = new Pizza();
            model.Categories = categories;

            return View("Create", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            _myLogger.WriteLog("Admin visit edit new pizza page", "EDIT");

            Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return View(pizzaToEdit);
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

            Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToEdit != null)
            {
                pizzaToEdit.Name = data.Name;
                pizzaToEdit.Description = data.Description;
                pizzaToEdit.Price = data.Price;
                pizzaToEdit.PhotoUrl = data.PhotoUrl;

                _myDatabase.SaveChanges();

                return RedirectToAction("Details", "Pizza", new { id = pizzaToEdit.Id });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _myLogger.WriteLog($"Admin delete pizza with {id}", "DELETE");

            Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToDelete != null)
            {
                _myDatabase.Pizzas.Remove(pizzaToDelete);
                _myDatabase.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
