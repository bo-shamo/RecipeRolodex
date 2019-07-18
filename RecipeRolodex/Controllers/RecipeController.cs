using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeRolodex.Data;
using RecipeRolodex.Models;
using RecipeRolodex.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeRolodex.Controllers
{
    public class RecipeController : Controller
    {

        //Database Initalization in website
        private ApplicationDbContext context;

        public RecipeController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        //(Eventual) Users Storage Homepage
        public IActionResult Index()
        {
            List<Recipe> recipes = context.Recipes.ToList();
            return View(recipes);
        }

        //Add a Recipe to your list
        public IActionResult Add()
        {
            AddEditRecipeViewModel addEditRecipeViewModel = new AddEditRecipeViewModel();
            return View(addEditRecipeViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEditRecipeViewModel addEditRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                //Make recipe 
                var newRecipe = AddEditRecipeViewModel.CreateRecipe(addEditRecipeViewModel);
                context.Recipes.Add(newRecipe);
                context.SaveChanges();

                
                //Make Ingredients
                string[] ingredients = addEditRecipeViewModel.Ingredients.Split(",");
                foreach (var ingredient in ingredients)
                {
                    var newIngredient = AddEditRecipeViewModel.CreateIngredient(ingredient, newRecipe.ID);
                    context.Ingredients.Add(newIngredient);
                }
                
                context.SaveChanges();
                return Redirect("/");
            }

            return View(addEditRecipeViewModel);
        }

        //Edit a recipe or after you completed one
        public IActionResult Edit(int recipeId)
        {
            
            //Get the recipe that is assoitated with the id
            IList<Ingredient> editIngredients = context.Ingredients.Include(p => p.Recipe).Where(p => p.RecipeID == recipeId).ToList();

            //Create a AddEditRecipeViewModel
            AddEditRecipeViewModel editRecipe = AddEditRecipeViewModel.ConvertToViewModel(editIngredients);

            //For now delete the entires out of the database
            foreach (Ingredient removeIngredient in editIngredients)
            {
                context.Ingredients.Remove(removeIngredient);
            }
            context.SaveChanges();

            //pass that into the view
            return View(editRecipe);
            
        }

        //Submittion of the edit
        [HttpPost]
        public IActionResult Edit(AddEditRecipeViewModel addEditRecipeViewModel)
        {
            //Check if edit was successful
            if (ModelState.IsValid)
            {
                var editRecipe = AddEditRecipeViewModel.CreateRecipe(addEditRecipeViewModel);
                context.Recipes.Update(editRecipe);

                //Make Ingredients
                string[] ingredients = addEditRecipeViewModel.Ingredients.Split(",");
                foreach (var ingredient in ingredients)
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

        //Show one Recipe in detail
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
        //Remove recipes from your account
        public IActionResult Remove()
        {
            IList<Recipe> removeRecipes = context.Recipes.ToList();
            RemoveRecipeViewModel removeRecipeViewModel = new RemoveRecipeViewModel()
            {
                Recipes = removeRecipes
            };
            return View(removeRecipeViewModel);
        }

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
    }
}
