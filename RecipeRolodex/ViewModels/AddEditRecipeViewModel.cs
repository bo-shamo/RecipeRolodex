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

        //Ingredient Class handlers
        //Stores ingredient id for edit view
        public IList<int> IngredientsID { get; set; }
        //Stores ingredient name for edit view
        public IList<string> IngredientsName { get; set; }

        //stores ingredients added by the new javascript rows
        public IList<string> NewIngredientsName { get; set; }

        /// <summary>
        /// Creates a blank view model for the intial display form
        /// </summary>
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

        /// <summary>
        /// For http post request from an edit form
        /// </summary>
        /// <param name="addEditRecipeViewModel"></param>
        /// <returns>recipe object</returns>
        public static Recipe CreateRecipe(AddEditRecipeViewModel addEditRecipeViewModel)
        {
            
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

        //Creates each individual ingredient
        public static Ingredient CreateIngredient(string oneIngredient, int recipeID)
        {
            Ingredient ingredient = new Ingredient
            {
                Name = oneIngredient,
                RecipeID = recipeID
            };
            return ingredient;
        }

        //Creates an addEditRecipeViewModel from a recipe class constructor
        //For edit view
        public static AddEditRecipeViewModel ConvertToViewModel(IList<Ingredient> recipe)
        {

            IList<int> ingredientID = new List<int>();
            IList<string> ingredientName = new List<string>();
            foreach (Ingredient ingredient in recipe)
            {
                ingredientID.Add(ingredient.ID);
                ingredientName.Add(ingredient.Name);

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
                IngredientsID = ingredientID,
                IngredientsName = ingredientName
            };
            return viewModel;
        }

    }
}
