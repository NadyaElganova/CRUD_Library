using CRUD_Library.Models;

namespace CRUD_Library.Extensions
{
    public static class DbContextExtensions
    {
        public static void UpdateManyToMany<T,KEY>(this AppDbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, KEY> getKey) where T : class
        {
            if (currentItems != null)
            {
                // cur      => [one , two, zero, three]
                // newItems => [one , three, four]
                db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
                db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
            }
            else
            {
                db.Set<T>().AddRange(newItems);
            }

        }

        public static IEnumerable<T> Except<T,KEY>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, KEY> getKeyFunc)
        {
            return items.GroupJoin(other, getKeyFunc, getKeyFunc, (item, templateItems) => new { item, templateItems })
            .SelectMany(t => t.templateItems.DefaultIfEmpty(), (t, tmp) => new { t, tmp })
            .Where(t => ReferenceEquals(null, t.tmp) || t.tmp.Equals(default(T))) //filter
            .Select(t => t.t.item);
        }
    }
}
