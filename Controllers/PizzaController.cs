using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using la_mia_pizzeria_static.Data;
using Microsoft.IdentityModel.Tokens;
using la_mia_pizzeria_post.Models;
using Microsoft.EntityFrameworkCore;

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
            pizzasCategories.Categories = _categories;
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
            pizzaToCreate.CategoryId = formPizza.Pizza.CategoryId;

            _db.Pizzas.Add(pizzaToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> myMenu = new List<Pizza>();

            myMenu = _db.Pizzas.OrderBy(pizza => pizza.Name).Include(dbPizza => dbPizza.Category).ToList<Pizza>();


            return View("Index", myMenu);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza;

            pizza = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).Include(dbPizza => dbPizza.Category).First();

            return View("Show", pizza);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToUpdate = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).First();
            pizzasCategories.Pizza = pizzaToUpdate;

            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                return View("Update", pizzasCategories);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,PizzasCategories formPizza)
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
                pizzaToUpdate.Name = formPizza.Pizza.Name;
                pizzaToUpdate.Description = formPizza.Pizza.Description;
                pizzaToUpdate.Image = formPizza.Pizza.Image;
                pizzaToUpdate.Price = formPizza.Pizza.Price;
                pizzaToUpdate.CategoryId = formPizza.Pizza.CategoryId;
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
