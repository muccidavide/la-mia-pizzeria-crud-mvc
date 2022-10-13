using la_mia_pizzeria_crud_mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_post.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        [Required(ErrorMessage = "Il campo è necessario")]
        [StringLength(200)]
        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
        public Ingredient()
        {

        }
    }

   
}
