using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeRolodex.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }

        //future will add amount and vol prop to facilitate imperial/metric converstion

        public IList<Recipe> Recipes { get; set; }

    }
}
