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
            // Temporal for Debugging purpose
            var t1 = assembly.GetExportedTypes();
            var t2 = t1.Where(t => t.GetInterfaces().Any());

            var mapFromTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var mapFromType in mapFromTypes)
            {
                var instance = Activator.CreateInstance(mapFromType);

                // Temporal for Debugging purpose
                var t5 = mapFromType.GetMethod("Map");
                var t6 = mapFromType.GetInterface("IMapFrom`1");
                var t7 = mapFromType.GetInterface("IMapFrom`1")!;
                var t8 = mapFromType.GetInterface("IMapFrom`1")!.GetMethod("Map");


                var mappingMethod = mapFromType.GetMethod("Map") ?? mapFromType.GetInterface("IMapFrom`1")!.GetMethod("Map");
                mappingMethod?.Invoke(instance, new object[] { this });
            }
        }


    }
}
