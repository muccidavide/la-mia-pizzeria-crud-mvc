using la_mia_pizzeria_post.Models;
using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace la_mia_pizzeria_post.Controllers
{
    public class IngredientController : Controller
    {
        PizzaContext _db;
        List<Ingredient> _ingredients;
        IngredientIngredientsList ingredientIngredientsList= new IngredientIngredientsList();

        public IngredientController()
        {
            _db = new PizzaContext();
            this._ingredients = _db.Ingredients.OrderBy(ingredient => ingredient.Name).ToList<Ingredient>();
            ingredientIngredientsList.Ingredients = _ingredients;
            ingredientIngredientsList.Ingredient = new Ingredient();
        }
        // GET: IngredientController
        public ActionResult Index()
        {

            return View(ingredientIngredientsList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientIngredientsList ingredientData)
        {
            if (!ModelState.IsValid)
            {
                return View(ingredientIngredientsList);
            }
            try
            {
                _db.Ingredients.Add(ingredientData.Ingredient);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("StoreDataExcetipn", ex.InnerException.Message);

                return View("Index", ingredientIngredientsList);

            }


            return View("Index", ingredientIngredientsList);
        }
    }
    
}
