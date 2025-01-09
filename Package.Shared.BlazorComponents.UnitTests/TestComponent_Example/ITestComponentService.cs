using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.UnitTests.TestComponent_Example
{
    public interface ITestComponentService
    {
        public string TestString { get; set; }
        public bool TestBool { get; set; }
    }
}
