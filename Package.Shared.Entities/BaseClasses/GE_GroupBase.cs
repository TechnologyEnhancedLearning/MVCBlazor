using Package.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.BaseClasses
{
    public abstract class GE_GroupBase : IGE_Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Deleted { get; set; }
        public Guid ClientTemporaryId { get; set; }

        public List<Type> Collection { get; set; }

        // Constructor that initializes all properties
        protected GE_GroupBase(int id, string title, bool deleted, List<Type> collection)
        {
            Id = id;
            Title = title;
            Deleted = deleted;
            ClientTemporaryId = Guid.NewGuid(); // Automatically generates a new Guid
            Collection = collection;
        }
    }
}
