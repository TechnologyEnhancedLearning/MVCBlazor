using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.UnitTests.TestComponent_Example
{

    public class TestComponentService : ITestComponentService
    {
        public string TestString { get; set; } = "TestStringDefault";
        public bool TestBool { get; set; } = false;
    }
}
