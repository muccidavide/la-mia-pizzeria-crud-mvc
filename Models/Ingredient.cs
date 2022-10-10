using la_mia_pizzeria_crud_mvc;

namespace la_mia_pizzeria_post.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
        public Ingredient()
        {

        }
    }

   
}
