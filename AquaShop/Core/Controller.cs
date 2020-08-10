using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        //decorations - DecorationRepository  
        //aquariums - collection of IAquarium
        private DecorationRepository decorations;
        private ICollection<IAquarium> aquariums;

        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }




        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType != "FreshwaterAquarium" && aquariumType != "SaltwaterAquarium")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            // FreshwaterAquarium" and "SaltwaterAquarium
            IAquarium aquarium = null;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            this.aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }



        public string AddDecoration(string decorationType)
        {
            //  Valid types are: "Ornament" and "Plant".
            if (decorationType != "Ornament" && decorationType != "Plant")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }
            IDecoration decoration = null;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            this.decorations.Add(decoration);
            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);


        }



        public string InsertDecoration(string aquariumName, string decorationType)
        {
            string result = string.Empty;
            IDecoration decorationToLookingFor = this.decorations.FindByType(decorationType);
            if (decorationToLookingFor == null)
            {
                string msg = string.Format(ExceptionMessages.InexistentDecoration, decorationType);
                throw new InvalidOperationException(msg);
            }
            else
            {
                // ima takava dekoraciq AMA ima li takav akwaruim w lista s akvariumi???????

                IAquarium auqriumWhereToInsertDecoration = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

                auqriumWhereToInsertDecoration.AddDecoration(decorationToLookingFor);
                this.decorations.Remove(decorationToLookingFor);
                // "Successfully added {decorationType} to {aquariumName}
                result = string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
            }
            return result;
        }






        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            // "FreshwaterFish", "SaltwaterFish". 
            if (fishType != "FreshwaterFish" && fishType != "SaltwaterFish")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }
            // SaltwaterFish Can only live in SaltwaterAquarium! 
            // FreshwaterFish Can only live in FreshwaterAquarium!  
            string result = string.Empty;

            IAquarium aqurium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);
            string type = aqurium.GetType().Name;
            if (
                (fishType == "SaltwaterFish" && type == "FreshwaterAquarium") ||
                (fishType == "FreshwaterFish" && type == "SaltwaterAquarium")
                )
            {
                result = OutputMessages.UnsuitableWater;
            }
            else
            {
                IFish fish = null;
                if (fishType == "SaltwaterFish")
                {
                    fish = new SaltwaterFish(fishName, fishSpecies, price);
                }
                else
                {
                    fish = new FreshwaterFish(fishName, fishSpecies, price);
                }
                aqurium.AddFish(fish);
                //Successfully added {fishType} to {aquariumName}
                result = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            return result;
        }




        public string FeedFish(string aquariumName)
        {
            IAquarium auqriumToFeed = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);
            auqriumToFeed.Feed();
            int fedCount = auqriumToFeed.Fish.Count;
            return string.Format(OutputMessages.FishFed, fedCount);
        }



        public string CalculateValue(string aquariumName)
        {
            IAquarium auqrium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);
            decimal valueResult = 0m;
            //It is calculated by the sum of all Fish’s and Decorations’ prices in the Aquarium. 
            foreach (IFish fish in auqrium.Fish)
            {
                valueResult += fish.Price;
            }

            foreach (IDecoration decoration in auqrium.Decorations)
            {
                valueResult += decoration.Price;
            }

            return string.Format(OutputMessages.AquariumValue, aquariumName, valueResult);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IAquarium currAquarium in this.aquariums)
            {
                sb.AppendLine(currAquarium.GetInfo());
            }
            return sb.ToString().Trim();
        }
    }
}
