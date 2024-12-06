using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Interfaces
{
    public interface IGE_Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public bool Deleted { get; set; }
        public Guid ClientTemporaryId { get; set; }

        public virtual string ToString() => $"{FirstName} {SecondName}";
    }
   
}

