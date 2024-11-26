using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Interfaces.ComponentInterfaces
{
    public interface IGE_VCB_RadioItem<TValue>
    {
        public TValue Value { get; }

        public string Label { get; }

        public bool Selected { get; set; }

        public string HintText { get; }
    }
}
