using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public class SaltwaterAquarium : Aquarium
    {
        //   Has 25 capacity
        private const int capacity = 25;
        public SaltwaterAquarium(string name) : base(name, capacity)
        {
        }
    }
}
