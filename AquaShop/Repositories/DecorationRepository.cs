using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly ICollection<IDecoration> models;
        public DecorationRepository()
        {
            this.models = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => (IReadOnlyCollection<IDecoration>)this.models;

        public void Add(IDecoration model)
        {
            this.models.Add(model);
        }

        public bool Remove(IDecoration model)
        {
            return this.models.Remove(model);
        }


        public IDecoration FindByType(string type)
        {
            IDecoration decorationToReturn = this.models.FirstOrDefault(x => x.GetType().Name == type);
            return decorationToReturn;
        }

        
    }
}
