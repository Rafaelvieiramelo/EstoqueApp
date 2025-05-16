namespace LidyDecorApp.Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static bool HasValue<T>(this IEnumerable<T>? source) => source != null && source.Any();
        public static bool HasNotValue<T>(this IEnumerable<T>? source) => source == null || !source.Any();
    }
}
