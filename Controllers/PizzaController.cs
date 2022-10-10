using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using la_mia_pizzeria_static.Data;
using Microsoft.IdentityModel.Tokens;
using la_mia_pizzeria_post.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        PizzaContext _db;
        List<Category> _categories;
        List<Ingredient> _ingredients;
        PizzasCategories pizzasCategories;

        public PizzaController()
        {
            this._db = new PizzaContext();
            this._categories = _db.Categories.ToList();
            this._ingredients = _db.Ingredients.ToList();
            this.pizzasCategories = new PizzasCategories();
            pizzasCategories.Categories = _categories;
            pizzasCategories.Ingredients = _ingredients;
        }
        /*
         CREATE
         */
        public IActionResult Create()
        {
            pizzasCategories.Categories = _categories;
            pizzasCategories.Ingredients = _ingredients;
            return View("Create", pizzasCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzasCategories formPizza)
        {

            if (!ModelState.IsValid)
            {
                formPizza.Categories = _categories;
                formPizza.Ingredients = _ingredients;
                return View("Create", formPizza);
            }

            formPizza.Pizza.Ingredients = _db.Ingredients.Where(ingredient => formPizza.SelectedIngredients.Contains(ingredient.IngredientId)).ToList<Ingredient>();

            try
            {
                _db.Pizzas.Add(formPizza.Pizza);
                _db.SaveChanges();
            }
            catch (SqlException ex)
            {
                ModelState.AddModelError("StoreDataExcetipn", ex.Message);
                formPizza.Categories = _categories;
                return View("Create", formPizza);

            }
            return RedirectToAction("Index");
        }

        /*
         INDEX & DETAILS
         */
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> myMenu = new List<Pizza>();

            myMenu = _db.Pizzas.OrderBy(pizza => pizza.Name).Include("Category").ToList<Pizza>();


            return View("Index", myMenu);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza;

            pizza = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).Include(dbPizza => dbPizza.Category).First();

            return View("Show", pizza);
        }

        /*
         UPDATE
         */
        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToUpdate = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).Include(dbPizza => dbPizza.Category).Include(dbPizza => dbPizza.Ingredients).First();


            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                pizzasCategories.Pizza = pizzaToUpdate;

                return View("Update", pizzasCategories);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzasCategories formPizza)
        {
            if (!ModelState.IsValid)
            {
                formPizza.Categories = _categories;
                formPizza.Ingredients = _ingredients;
                return View("Update", formPizza);
            }

            Pizza pizzaToUpdate = _db.Pizzas.Where(dbPizza => dbPizza.PizzaId == id).Include("Ingredients").First();

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
                pizzaToUpdate.Ingredients = _db.Ingredients.Where(ingredient => formPizza.SelectedIngredients.Contains(ingredient.IngredientId)).ToList<Ingredient>();

                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("StoreDataExcetipn", ex.Message);
                    formPizza.Categories = _categories;
                    formPizza.Ingredients = _ingredients;
                    return View("Update", formPizza);

                }

                return RedirectToAction("Index");
            }

        }

        /*
         DELETE
         */
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