using System.Reflection;

namespace Backend.Shared
{
    public static class FormMapper
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> proplists = new();

        public static T Map<T>(IFormCollection form) where T : new()
        {
            if (!proplists.ContainsKey(typeof(T)))
                proplists[typeof(T)] = typeof(T).GetProperties().ToList();

            T item = new();

            proplists[typeof(T)].ForEach((prop) =>
            {
                string name = prop.Name;
                Type type = prop.PropertyType;

                string? res = null;
                IFormFile? file = null;

                if (form.ContainsKey(name))
                {
                    res = form[name];
                }
                else if (form.ContainsKey(name.ToLower()))
                {
                    res = form[name.ToLower()];
                }
                else
                {
                    var f = form.Files.GetFile(name) ?? form.Files.GetFile(name.ToLower());
                    if (f is not null) file = f;
                }

                if (string.IsNullOrEmpty(res) && file is null) return;

                object obj;
                if (file is null)
                {
                    obj = Convert.ChangeType(res, type);
                }
                else if (file is IFormFile)
                {
                    obj = file;
                }
                else
                {
                    return;
                }

                prop.SetValue(item, obj);
            });

            return item;
        }
    }
}