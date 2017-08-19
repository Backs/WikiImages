using System;
using Unity;
using WikiImages.Infrastructure.Services;
using WikiImages.Infrastructure.Services.Interfaces;

namespace WikiImages.Infrastructure
{
    public sealed class InfrastructureContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IUserLocationService, StubLocationService>(new ContainerControlledLifetimeManager());
        }
    }
}
