using la_mia_pizzeria_crud_mvc;

namespace la_mia_pizzeria_post.Models
{
    public class IngredientIngredientsList
    {
        public IngredientIngredientsList()
        {
            Ingredient = new Ingredient();
            Ingredients = new List<Ingredient>();
        }

        public Ingredient Ingredient { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
