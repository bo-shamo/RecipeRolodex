using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeRolodex.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRolodex.ViewModels
{
    public class AddEditRecipeViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        //[Range(10,1500,ErrorMessage="Please enter a cooking time between 10 minutes and 1500 minutes (24 hours)")]
        public double Time { get; set; }

        public int Serve { get; set; }

        [Required]
        public string Source { get; set; }

        //An enum of types of dishes that the user can select from
        //Store the Choice of type
        [Required]
        public RecipeType Type { get; set; }

        //Hold the possible choices
        public List<SelectListItem> RecipeTypes { get; set; }

        //One Recipe to Many Ingredients relationship
        public string Ingredients { get; set; }
        //public int IngredientID { get; set; }

        //public Ingredient Ingredient { get; set; }

        public AddEditRecipeViewModel()
        {
            RecipeTypes = new List<SelectListItem>();
            foreach (string str in Enum.GetNames(typeof(RecipeType)))
            {
                RecipeTypes.Add(new SelectListItem
                {
                    Value = str,
                    Text = str
                });
                
            }
        
        }

        public static Recipe CreateRecipe(AddEditRecipeViewModel addEditRecipeViewModel)
        {
            //If the user types in
            if (addEditRecipeViewModel.Time < 24)
            {
                addEditRecipeViewModel.Time = addEditRecipeViewModel.Time*60;
            }
            Recipe newrecipe = new Recipe
            {
                ID = addEditRecipeViewModel.ID,
                Title = addEditRecipeViewModel.Title,
                Description = addEditRecipeViewModel.Description,
                Type = addEditRecipeViewModel.Type,
                Time = (int)addEditRecipeViewModel.Time,
                Serve = addEditRecipeViewModel.Serve,
                Source = addEditRecipeViewModel.Source
            };
            return newrecipe;
        }

        //Creates each individual ingredient, need to find a better way to do this
        public static Ingredient CreateIngredient(string oneIngredient, int recipeID)
        {
            Ingredient ingredient = new Ingredient
            {
                Name = oneIngredient,
                RecipeID = recipeID
            };
            return ingredient;
        }

        //Creates and addEditRecipeViewModel from a recipe class constructor
        public static AddEditRecipeViewModel ConvertToViewModel(IList<Ingredient> recipe)
        {
            //Create Ingredient string using string builder
            StringBuilder sb = new StringBuilder();
            Boolean first = true;
            foreach (Ingredient ingredient in recipe)
            {
                if (first)
                {
                    sb.Append(ingredient.Name);
                    first = false;
                }
                else
                {
                    sb.Append("," + ingredient.Name);
                }
                
            }
            //Recreate accurate Time
            if(recipe[0].Recipe.Time > 120)
            {
                recipe[0].Recipe.Time = recipe[0].Recipe.Time / 60;
            }
            //Put it all in the ViewModel
            AddEditRecipeViewModel viewModel = new AddEditRecipeViewModel
            {
                ID = recipe[0].RecipeID,
                Title = recipe[0].Recipe.Title,
                Description = recipe[0].Recipe.Description,
                Type = recipe[0].Recipe.Type,
                Time = recipe[0].Recipe.Time,
                Serve = recipe[0].Recipe.Serve,
                Source = recipe[0].Recipe.Source,
                Ingredients = sb.ToString()
            };
            return viewModel;
        }

    }
}
