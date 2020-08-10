using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        //Has 50 capacity.
        private  const int capacity = 50;
        public FreshwaterAquarium(string name) : base(name, capacity)
        {
        }
    }
}
