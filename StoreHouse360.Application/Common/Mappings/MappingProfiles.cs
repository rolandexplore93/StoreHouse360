using AutoMapper;
using System.Reflection;

namespace StoreHouse360.Application.Common.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles(Assembly[] assemblies)
        {
            AddProfiles(assemblies);
        }

        private void AddProfiles(Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                AddAssemblyProfiles(assembly);
            }
        }

        private void AddAssemblyProfiles(Assembly assembly)
        {
            var t1 = assembly.GetExportedTypes();
            var t2 = t1.Where(t => t.GetInterfaces().Any());

            var mapFromTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

            foreach (var mapFromType in mapFromTypes)
            {
                var instance = Activator.CreateInstance(mapFromType);
                var mappingMethod = mapFromType.GetMethod("Map")
                                    ?? mapFromType.GetInterface("IMapFrom`1")!.GetMethod("Map");
                mappingMethod?.Invoke(instance, new object[] { this });
            }
        }


    }
}
