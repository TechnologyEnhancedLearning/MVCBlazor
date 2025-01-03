using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.UnitTests.DependencyInjection
{
    /// <summary>
    /// qqqq really uncertain of this needs alot of testing
    /// </summary>
    public class FallbackServiceProvider : IServiceProvider
    {
        private readonly Fixture _fixture = new Fixture();
        public FallbackServiceProvider()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }
        public object? GetService(Type serviceType)
        {
            try
            {
                return _fixture.Create(serviceType, new SpecimenContext(this._fixture));
            }
            catch
            {
                return null; // Return null if AutoFixture cannot create the service
            }
        }
    }


}
