using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeRolodex.Models
{
    public class Recipe
    {
        //All Varables needed to represent a recipe object
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public int Serve { get; set; }
        public string Source { get; set; }
        //An enum of types of dishes that the user can select from
        public RecipeType Type { get; set; }
        //One Recipe to Many Ingredients relationship
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }

        //Constructors
        public Recipe() { }



        
    }
}
