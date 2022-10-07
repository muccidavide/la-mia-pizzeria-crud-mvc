using la_mia_pizzeria_crud_mvc;

namespace la_mia_pizzeria_post.Models
{
    public class PizzasCategories
    {
        public PizzasCategories()
        {
            Pizza = new Pizza();
            Categories = new List<Category>();
        }

        public Pizza Pizza { get; set; }

        public List<Category> Categories { get; set; }  

    }
}
