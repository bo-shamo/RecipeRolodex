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

        /*future will add amount and vol prop to facilitate imperial/metric converstion
        public double Amount {get; set; }
        public boolean IsVol {get; set; }
        */

        //One Recipe for many Ingredients
        //Foreign Key for what recipe this ingredient is a part of
        public int RecipeID { get; set; }
        //for search to store that recipe internally
        public Recipe Recipe { get; set; }

        public Ingredient() { }

    }
}
