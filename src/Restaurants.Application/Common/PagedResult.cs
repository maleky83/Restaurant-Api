namespace Restaurants.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(
            IEnumerable<T> items, int totalItemsCount, int? pageSize, int? pageNumber)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
            TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize.GetValueOrDefault());
            ItemsFrom = (pageSize.GetValueOrDefault() * (pageNumber.GetValueOrDefault() - 1)) + 1;
            ItemsTo = ItemsFrom + items.Count() - 1;
        }
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
