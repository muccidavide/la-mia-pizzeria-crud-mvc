using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using la_mia_pizzeria_static.Data;
using Microsoft.IdentityModel.Tokens;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        PizzaContext _db;

        public PizzaController()
        {
            this._db = new PizzaContext();
        }

        public IActionResult Create()
        {
            return View("Create");
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

            /// FAKE DB
            //Pizza pizzaMargherita = new("margherita", "pizza margherita", "/img/img-1.jpg", 13.30f);
            //Pizza pizzaNapoli = new("napoli", "pizza napoli", "/img/img-2.jpeg", 10.00f);
            //Pizza pizzaBufala = new("bufala", "pizza bufala", "/img/img-3.jpeg", 15.50f);
            //Pizza pizzaMarinara = new("marinara", "pizza marinara", "/img/img-4.jpg", 12.30f);
            //Pizza pizzaQuattroFormaggi = new("quattro formaggi", "pizza quatro formaggi", "/img/img-5.jpeg", 13.00f);
            //Pizza pizzaBianca = new("bianca", "pizza bianca", "/img/img-6.jpeg", 15.50f);
            //Pizza pizzaOliveeAcciughe = new("olive e acciughe", "pizza olive e acciughe", "/img/img-7.jpeg", 12.30f);
            //Pizza pizzaRadicchio = new("raddicchio", "pizza raddicchio", "/img/img-8.jpeg", 13.00f);
            //Pizza pizzaGenovese = new("genovese", "pizza genovese", "/img/img-9.jpg", 15.50f);

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
                return View("Update");
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

        [HttpDelete]
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
