using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using la_mia_pizzeria_static.Data;
using Microsoft.IdentityModel.Tokens;
using la_mia_pizzeria_post.Models;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        PizzaContext _db;
        List<Category> _categories;
        PizzasCategories pizzasCategories;

        public PizzaController()
        {
            this._db = new PizzaContext();
            this._categories = _db.Categories.ToList();
            this.pizzasCategories = new PizzasCategories(); 
        }

        public IActionResult Create()
        {
            pizzasCategories.Categories = _categories;
            return View("Create", pizzasCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzasCategories formPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formPizza);
            }

            Pizza pizzaToCreate = new Pizza();
            pizzaToCreate.Name = formPizza.Pizza.Name;
            pizzaToCreate.Description = formPizza.Pizza.Description;
            pizzaToCreate.Image = formPizza.Pizza.Image;
            pizzaToCreate.Price = formPizza.Pizza.Price;

            _db.Pizzas.Add(pizzaToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> myMenu = new List<Pizza>();

            myMenu = _db.Pizzas.OrderBy(pizza => pizza.Name).ToList<Pizza>();

            return View("Index", myMenu);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza;

            pizza = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).First();

            return View("Show", pizza);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToUpdate = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).First();

            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                return View("Update", pizzaToUpdate);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,Pizza formPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", formPizza);
            }

            Pizza pizzaToUpdate = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).First();

            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                pizzaToUpdate.Name = formPizza.Name;
                pizzaToUpdate.Description = formPizza.Description;
                pizzaToUpdate.Image = formPizza.Image;
                pizzaToUpdate.Price = formPizza.Price;
                _db.SaveChanges();
                return RedirectToAction("Index");   
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizzaToRemove = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).First();

            if (pizzaToRemove == null)
            {
                return NotFound();
            }
            _db.Pizzas.Remove(pizzaToRemove);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
