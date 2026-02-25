using System.Text.Json;

namespace mvc.starter.template.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T source)
        {
            if (source == null)
                return default!;

            var json = JsonSerializer.Serialize(source);

            return JsonSerializer.Deserialize<T>(json)!;
        }
    }
}
