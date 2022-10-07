using la_mia_pizzeria_crud_mvc;

namespace la_mia_pizzeria_post.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Pizza> Pizza { get; set; }

        public Category()
        {
            
        }
    }
}
