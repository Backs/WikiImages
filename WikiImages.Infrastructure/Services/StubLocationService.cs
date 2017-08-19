using WikiImages.Infrastructure.Services.Interfaces;

namespace WikiImages.Infrastructure.Services
{
    internal sealed class StubLocationService : IUserLocationService
    {
        public Location GetCurrentlocation()
        {
            return new Location(55.023525, 82.941754);
        }
    }
}
