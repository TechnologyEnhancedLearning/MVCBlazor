using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Components;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BUnit.UnitTests.DependencyInjection
{
    /// <summary>
    /// qqqq really uncertain of this needs alot of testing
    /// Not really sure what the fallback acheives suppose we dont have breaking concrete service where not used
    /// we tend to be using them though
    /// and we are generally injecting by interface
    /// </summary>
    public class FallbackServiceProvider : IServiceProvider
    {
        private readonly Fixture _fixture;

        public FallbackServiceProvider()
        {
            // Initialize AutoFixture and configure it with Moq
            _fixture = new Fixture();
            _fixture.Customize(new AutoFixture.AutoMoq.AutoMoqCustomization { ConfigureMembers = true });
        }

        public object? GetService(Type serviceType)
        {
            try
            {

                // Prevent AutoFixture from mocking IComponentActivator, which can cause issues
                if (serviceType == typeof(IComponentActivator))
                {
                    return null; // Let Blazor's default DI handle this
                }

                //if (serviceType.IsInterface)
                //{
                // qqqq https://bunit.dev/docs/providing-input/inject-services-into-components.html#using-libraries-like-automocker-as-fallback-provider
                //    return _fixture.Create(serviceType);
                //}

                // If it's not an interface, try creating the real object
                return _fixture.Create(serviceType, new SpecimenContext(this._fixture));
            }
            catch
            {
                // Return null if AutoFixture can't create the service
                return null;
            }
        }


    }
}
