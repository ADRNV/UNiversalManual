namespace UMan.Core.Pagination
{
    public abstract class Page<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public Page(IEnumerable<T> items, int totalCount, string? error = null)
        {
            Items = items;

            TotalCount = totalCount;

            Error = error;
        }

        public int TotalCount { get; }

        public string? Error { get; }

        public int Count
        {
            get => Items.Count();
        }
    }
}
