using RecipeRolodex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeRolodex.ViewModels
{
    public class DetailRecipeViewModel
    {
        //All the information I want to show in the View Form
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public int Serve { get; set; }
        public string Source { get; set; }
        //An enum of types of dishes that the user can select from
        public RecipeType Type { get; set; }

        //One Recipe to Many Ingredients relationship
        //For storing the list of ingredients
        public IList<Ingredient> Ingredients { get; set; }

        public DetailRecipeViewModel(IList<Ingredient> ingredients)
        {
            ID = ingredients[0].Recipe.ID;
            Title = ingredients[0].Recipe.Title;
            Description = ingredients[0].Recipe.Description;
            Time = ingredients[0].Recipe.Time;
            Serve = ingredients[0].Recipe.Serve;
            Source = ingredients[0].Recipe.Source;

            //Create Ingredient list
            Ingredients = ingredients;

        }


    }
}
