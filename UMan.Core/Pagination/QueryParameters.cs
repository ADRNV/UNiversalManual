namespace UMan.Core.Pagination
{
    public class QueryParameters
    {
        const int MaxItemsPerPage = 100;

        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;

            set
            {
                if (value <= 0)
                {
                    _pageSize = 1;
                }
                else if (value > MaxItemsPerPage)
                {
                    _pageSize = MaxItemsPerPage;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
