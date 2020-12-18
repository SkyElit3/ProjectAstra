using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TypeBaseExtensions = Microsoft.EntityFrameworkCore.Metadata.Internal.TypeBaseExtensions;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static T UpdateByReflection<T>(this object inputObject, T updateModel) where T : class
        {
            if (!(inputObject is T returnValue))
                return default;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var rootValue = property.GetValue(returnValue);
                var updateValue = property.GetValue(updateModel);

                if (rootValue == null || rootValue.Equals(updateValue)) continue;
                if (updateModel != null) property.SetValue(returnValue, property.GetValue(updateModel));
            }

            return returnValue;
        }

        public static void ModelBuilderUniqueIndexReflection(this ModelBuilder inputBuilder)
        {
            inputBuilder.Model.GetEntityTypes().Where(t => t.ClrType != null).ToList().ForEach(entityType => inputBuilder.Entity(entityType.ClrType).HasIndex("Name").IsUnique());
        }
    }
}