using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Interfaces
{
    public interface IGE_Group
    {
        int Id { get; set; }
        string Title { get; set; }
        bool Deleted { get; set; }
        Guid ClientTemporaryId { get; set; }

        List<Type> Collection { get; set; }
    }
}
