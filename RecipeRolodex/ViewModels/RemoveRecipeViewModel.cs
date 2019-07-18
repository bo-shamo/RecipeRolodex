using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeRolodex.Models;

namespace RecipeRolodex.ViewModels
{
    public class RemoveRecipeViewModel
    {
        public IList<Recipe> Recipes { get; set; }

        public RemoveRecipeViewModel() { }
    }

    
}
