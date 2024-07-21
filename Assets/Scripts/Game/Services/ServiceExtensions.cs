using System.Collections.Generic;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Services
{
    internal static class ServiceExtensions
    {
        internal static IScreenSystemService GetScreenSystemService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IScreenSystemService>();
        }

        internal static IScreenSystemService GetScreenSystemService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IScreenSystemService>(UiServiceType.ScreenSystemService);
        }

        internal static TService GetService<TService>(this IReadOnlyList<IUiService> uiServices, UiServiceType serviceType) where TService : class, IService
        {
            foreach (var service in uiServices)
                if (service.UiServiceType == serviceType)
                    return service as TService;

            return null;
        }
    }
}
