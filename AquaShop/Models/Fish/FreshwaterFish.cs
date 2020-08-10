using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        // FreshwaterFish Can only live in FreshwaterAquarium! 
        private const int intitialSize = 3;
        public FreshwaterFish(string name, string species, decimal price)
            : base(name, species,price)
        {

            this.Size = intitialSize;
        }

        public override void Eat()
        {
            this.Size += 3;
        }
    }
}
