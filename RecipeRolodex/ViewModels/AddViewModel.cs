using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeRolodex.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeRolodex.ViewModels
{
    public class AddViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(10,1500,ErrorMessage="Please enter a cooking time between 10 minutes and 1500 minutes (24 hours)")]
        public int Time { get; set; }

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

        public AddViewModel()
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

        public static Recipe CreateRecipe(AddViewModel addViewModel)
        {
           
            Recipe newrecipe = new Recipe
            {
                Title = addViewModel.Title,
                Description = addViewModel.Description,
                Type = addViewModel.Type,
                Time = addViewModel.Time,
                Serve = addViewModel.Serve,
                Source = addViewModel.Source
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
    }
}
