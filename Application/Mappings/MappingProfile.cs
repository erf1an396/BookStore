using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Guid?, Guid>().ConvertUsing(guid => guid == null ? Guid.NewGuid() : guid.Value);

            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            System.Collections.Generic.List<Type> types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (Type type in types)
            {
                object instance = Activator.CreateInstance(type);

                MethodInfo methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
