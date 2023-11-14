using PashaBankApp.Services.Interface;
using PashaBankApp.Services;
using System.Reflection;

namespace PBG.Distributor.UI.Settings
{
    public static class ReflectionServices
    {
        public static void AddInjectServices(this IServiceCollection collection, Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            
            var asemb = AppDomain.CurrentDomain.Load("PBG.Distributor.Core");

            if (asemb == null) 
            {
                throw new Exception("Such a library no exists");
            }
            var assem = asemb.GetTypes().Where(i =>
                !i.IsAbstract &&
                !i.IsGenericTypeDefinition &&
                !i.IsInterface &&
                 i.Name.Contains("Services", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            foreach (var item in assem)
            {
               var interfaceService=item.GetInterfaces().ToList();
                if (interfaceService.Count() == 0)
                {
                    throw new Exception("No Interface exists");
                }
                foreach (var inter in interfaceService)
                {
                    collection.AddScoped(inter, item);
                }
            }

        }
    }
}
