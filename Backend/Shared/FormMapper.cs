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

                string res = string.Empty;

                if (form.ContainsKey(name))
                {
                    res = form[name];
                }

                if (string.IsNullOrEmpty(res) && form.ContainsKey(name.ToLower()))
                {
                    res = form[name.ToLower()];
                }

                if (string.IsNullOrEmpty(res)) return;

                var obj = Convert.ChangeType(res, type);

                prop.SetValue(item, obj);
            });

            return item;
        }
    }
}