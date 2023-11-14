using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualBasic;
using PashaBankApp.Services;
using PashaBankApp.Services.Interface;

namespace PBG.Distributor.UI.Settings
{
    public static class ReflectionHandler
    { 
        public static void AddInjectHandlers(this IServiceCollection collection, Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
         

            var assem = assembly.GetTypes().Where(i =>
                !i.IsAbstract &&
                ! i.IsGenericTypeDefinition &&
                !i.IsInterface &&
                i.Name.Contains("CommandHandler", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            foreach (var item in assem)
            {
               var interfaces=item.GetInterfaces().ToList();
                if (interfaces.Count()== 0)
                {
                    throw new Exception("No interface exist for this class");
                }
                foreach (var iface in interfaces)
                {
                    collection.AddTransient(iface, item);
                }

            }   

        }
    }
}