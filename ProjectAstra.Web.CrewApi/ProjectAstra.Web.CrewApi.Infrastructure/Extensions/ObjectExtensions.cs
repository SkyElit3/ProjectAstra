using System.Reflection;

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
    }
}