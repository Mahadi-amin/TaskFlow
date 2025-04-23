using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskFlow.Infrastructure.Utilities
{
    public static class RazorUtility
    {
        public static IList<SelectListItem> ToSelectList<TItem, TValue>
            (
            this IEnumerable<TItem> items,
            Func<TItem, string> itemName,
            Func<TItem, TValue> itemId
            )
        {
            var selectList = items.Select(item => new SelectListItem
            {
                Text = itemName(item),
                Value = itemId(item).ToString()
            })
                                .ToList();

            return selectList;
        }
    }
}
