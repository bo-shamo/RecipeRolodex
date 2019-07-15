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
            AddViewModel addViewModel = new AddViewModel();
            return View(addViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel addViewModel)
        {
            if (ModelState.IsValid)
            {
                //Make recipe 
                var newRecipe = AddViewModel.CreateRecipe(addViewModel);
                context.Recipes.Add(newRecipe);
                context.SaveChanges();

                
                //Make Ingredients
                string[] ingredients = addViewModel.Ingredients.Split(",");
                foreach (var ingredient in ingredients)
                {
                    var newIngredient = AddViewModel.CreateIngredient(ingredient, newRecipe.ID);
                    context.Ingredients.Add(newIngredient);
                }
                
                context.SaveChanges();
                return Redirect("/");
            }

            return View(addViewModel);
        }

        //Edit a recipe or after you completed one
        public IActionResult Edit()
        {
            return View();
        }

        //Show one Recipe in detail
        public IActionResult Select()
        {
            return View();
        }
        //Remove recipes from your account
        public IActionResult Remove()
        {
            return View();
        }
    }
}
