using Unity;
using WikiImages.Api.Services;
using WikiImages.Api.Services.Interfaces;

namespace WikiImages.Api
{
    public sealed class ApiContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IApiService, ApiService>(new PerResolveLifetimeManager());
        }
    }
}
