namespace UMan.Core.Pagination
{
    public abstract class Page<T>
    {
        private readonly IEnumerable<T> _items;
        public Page(IEnumerable<T> items, int totalCount, string? error = null)
        {
            _items = items;

            TotalCount = totalCount;

            Error = error;
        }

        public int TotalCount { get; }

        public string? Error { get; }

        public int Count
        {
            get => _items.Count();
        }
    }
}
