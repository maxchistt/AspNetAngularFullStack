using System.Reflection;

namespace Backend.Shared.Other
{
    public static class FormMapper
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> proplists = new();

        private static List<PropertyInfo> GetProps<T>() where T : new()
        {
            if (!proplists.ContainsKey(typeof(T)))
                proplists[typeof(T)] = typeof(T).GetProperties().ToList();
            return proplists[typeof(T)];
        }

        private static string? GetStringElement(this IFormCollection form, string name)
        {
            if (form.ContainsKey(name))
                return form[name];
            if (form.ContainsKey(name.ToLower()))
                return form[name.ToLower()];
            return null;
        }

        private static IFormFile? GetFileElement(this IFormCollection form, string name)
        {
            return form.Files.GetFile(name) ?? form.Files.GetFile(name.ToLower());
        }

        public static T Map<T>(this IFormCollection form) where T : new()
        {
            T item = new();

            foreach (var prop in GetProps<T>())
            {
                string name = prop.Name;
                Type type = prop.PropertyType;

                string? str = form.GetStringElement(name);
                IFormFile? file = str is null ? form.GetFileElement(name) : null;

                if (string.IsNullOrEmpty(str) && file is null) break;

                object value = file is null
                    ? Convert.ChangeType(str, type)!
                    : file;

                prop.SetValue(item, value);
            };

            return item;
        }

        public static bool Validate<T>(this IFormCollection form) where T : new()
        {
            foreach (var prop in GetProps<T>())
            {
                string name = prop.Name;

                bool contains = form.GetStringElement(name) is not null || form.GetFileElement(name) is not null;

                if (!contains) return false;
            }

            return true;
        }
    }
}