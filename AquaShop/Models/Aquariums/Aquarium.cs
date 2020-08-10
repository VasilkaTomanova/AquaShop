using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private ICollection<IDecoration> decorations;
        private ICollection<IFish> fishes;
        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.decorations = new List<IDecoration>();
            this.fishes = new List<IFish>();
        }


        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                this.name = value;
            }
        }
        public int Capacity
        {
            get;
            private set;

        }

        public int Comfort => this.decorations.Sum(x => x.Comfort);

        public ICollection<IDecoration> Decorations => this.decorations;

        public ICollection<IFish> Fish => this.fishes;


        public void AddFish(IFish fish)
        {
            if (this.fishes.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            this.fishes.Add(fish);
        }


        public bool RemoveFish(IFish fish)
        {
            return this.fishes.Remove(fish);

        }


        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }


        public void Feed()
        {
            foreach (IFish fish in this.fishes)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
            if (!this.fishes.Any())
            {
                sb.AppendLine("Fish: none");
            }
            else
            {
                sb.AppendLine($"Fish: {string.Join(", ", this.fishes.Select(x => x.Name))}");
            }

            sb.AppendLine($"Decorations: {this.decorations.Count}");
            sb.AppendLine($"Comfort: {this.Comfort}");


            return sb.ToString().Trim();
        }


    }
}
