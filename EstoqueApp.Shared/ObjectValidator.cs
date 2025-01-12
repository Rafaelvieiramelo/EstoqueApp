namespace EstoqueApp.Shared
{
    public static class ObjectValidator
    {
        public static bool IsObjectDefault(object obj)
        {
            if (obj == null) return true;

            foreach (var prop in obj.GetType().GetProperties())
            {
                var propValue = prop.GetValue(obj);
                var defaultValue = GetDefaultValue(prop.PropertyType);

                if (!object.Equals(propValue, defaultValue))
                {
                    return false;
                }
            }

            return true;
        }

        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
    }
}
