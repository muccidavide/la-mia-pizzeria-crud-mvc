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
        List<Category> Categories;

        public PizzaController()
        {
            this._db = new PizzaContext();
            this.Categories = _db.Categories.ToList();
        }

        public IActionResult Create()
        {
            return View("Create", Categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza formPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formPizza);
            }

            Pizza pizzaToCreate = new Pizza();
            pizzaToCreate.Name = formPizza.Name;
            pizzaToCreate.Description = formPizza.Description;
            pizzaToCreate.Image = formPizza.Image;
            pizzaToCreate.Price = formPizza.Price;

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
