using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeRolodex.Data;
using RecipeRolodex.Models;
using RecipeRolodex.ViewModels;

namespace RecipeRolodex.Controllers
{
    public class RecipeController : Controller
    {
        #region Database
        //Database Initalization in website
        private ApplicationDbContext context;

        public RecipeController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        #endregion
        #region Index
        //(Eventual) Users Storage Homepage
        public IActionResult Index()
        {
            List<Recipe> recipes = context.Recipes.ToList();
            return View(recipes);
        }
        #endregion
        #region Add
        //Add a Recipe to your list
        public IActionResult Add()
        {
            AddEditRecipeViewModel addEditRecipeViewModel = new AddEditRecipeViewModel();
            return View(addEditRecipeViewModel);
        }
        
        /// <summary>
        /// Receives the form submittion and checks validation
        /// </summary>
        /// <param name="addEditRecipeViewModel"></param>
        /// <returns>Either the form with errors displayed or the index view</returns>
        [HttpPost]
        public IActionResult Add(AddEditRecipeViewModel addEditRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                //Make recipe 
                var newRecipe = AddEditRecipeViewModel.CreateRecipe(addEditRecipeViewModel);
                context.Recipes.Add(newRecipe);
                context.SaveChanges();


                /*//Make Ingredients
                //TODO: make better ingredient handler with javascript
                string[] ingredients = addEditRecipeViewModel.Ingredients.Split(",");
                foreach (var ingredient in ingredients)
                {
                    var newIngredient = AddEditRecipeViewModel.CreateIngredient(ingredient, newRecipe.ID);
                    context.Ingredients.Add(newIngredient);
                }*/
                foreach (var ingredientName in addEditRecipeViewModel.IngredientsName)
                {
                    var newIngredient = AddEditRecipeViewModel.CreateIngredient(ingredientName, newRecipe.ID);
                    context.Ingredients.Add(newIngredient);
                }


                context.SaveChanges();
                return Redirect("/");
            }

            return View(addEditRecipeViewModel);
        }
        #endregion
        #region Edit
        /// <summary>
        /// Takes in the id for a recipe and converts it into the AddEditRecipeViewModel type for displaying
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>the edit form view with data already filled in to the form</returns>
        public IActionResult Edit(int recipeId)
        {
            
            //Get the recipe that is assoitated with the id
            IList<Ingredient> editIngredients = context.Ingredients.Include(p => p.Recipe).Where(p => p.RecipeID == recipeId).ToList();

            //Create a AddEditRecipeViewModel
            AddEditRecipeViewModel editRecipe = AddEditRecipeViewModel.ConvertToViewModel(editIngredients);

            //pass that into the view
            return View(editRecipe);
            
        }

        /// <summary>
        /// Recieves the form post request and handles the data handling to the model if there is no errors
        /// </summary>
        /// <param name="addEditRecipeViewModel"></param>
        /// <returns>either the form again with errors or the index view with edits saved</returns>
        [HttpPost]
        public IActionResult Edit(AddEditRecipeViewModel addEditRecipeViewModel)
        {
            //Check if edit was successful
            if (ModelState.IsValid)
            {
                var editRecipe = AddEditRecipeViewModel.CreateRecipe(addEditRecipeViewModel);
                context.Recipes.Update(editRecipe);

                //Make Ingredients
                //TODO:Check old ingredients and only add new ingredients and remove removed ingredients
                foreach (var ingredient in addEditRecipeViewModel.IngredientsName)
                {
                    var newIngredient = AddEditRecipeViewModel.CreateIngredient(ingredient, editRecipe.ID);
                    context.Ingredients.Add(newIngredient);
                }
                context.SaveChanges();
                return Redirect("/");
            }

            //reposted the form it there was errors
            return View(addEditRecipeViewModel);
        }
        #endregion
        #region Detail
        /// <summary>
        /// Shows an indepth view of the recipe so people can see all the details
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>Tf there is ingredients to the recipe, it returns the detailed view</returns>
        public IActionResult Detail(int recipeId)
        {
            //Get the list of Ingredients
            IList<Ingredient> editIngredients = context.Ingredients.Include(p => p.Recipe).Where(p => p.RecipeID == recipeId).ToList();
            if(editIngredients.Count() != 0)
            {
                DetailRecipeViewModel detailRecipeViewModel = new DetailRecipeViewModel(editIngredients);
                return View(detailRecipeViewModel);
            }
            else
            {
                return Redirect("/");
            }
            

        }
        #endregion
        #region Remove
        /// <summary>
        /// Grab all the recipes available in the database and shows them for removal
        /// </summary>
        /// <returns>view with all the recipes shown</returns>
        public IActionResult Remove()
        {
            IList<Recipe> removeRecipes = context.Recipes.ToList();
            RemoveRecipeViewModel removeRecipeViewModel = new RemoveRecipeViewModel()
            {
                Recipes = removeRecipes
            };
            return View(removeRecipeViewModel);
        }

        /// <summary>
        /// Takes an array of recipes to remove and takes out the recipes and their ingredients
        /// </summary>
        /// <param name="recipeIds"></param>
        /// <returns>index view with the selected recipes removed</returns>
        [HttpPost]
        public IActionResult Remove(int[] recipeIds)
        {
            if(recipeIds.Length != 0)
            {
                foreach (int recipeId in recipeIds)
                {
                    Recipe recipe = context.Recipes.Single(p => p.ID == recipeId);
                    context.Recipes.Remove(recipe);
                    List<Ingredient> ingredients = context.Ingredients.Where(p => p.RecipeID == recipe.ID).ToList();
                    foreach(var ingredient in ingredients)
                    {
                        context.Ingredients.Remove(ingredient);
                    }
                    
                }
                context.SaveChanges();
            }
            
            return Redirect("/");
        }
        #endregion
    }
}
