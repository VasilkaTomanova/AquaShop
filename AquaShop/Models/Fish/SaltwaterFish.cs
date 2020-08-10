using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        // Has 5 initial size.
        // SaltwaterFish Can only live in SaltwaterAquarium! 
        
        private const int intitialSize = 5;
        public SaltwaterFish(string name, string species, decimal price)
            : base(name, species,price)
        {
            this.Size = intitialSize;
        }
        public override void Eat()
        {
            this.Size += 2;
        }
    }
}
