namespace nowe_Restaurant_API.Models
{
    public class SearchQuery
    {
        public string? searchby { get; set; }
        public int pagesize { get; set; }
        public int pagenumber { get; set; }
        public string? sortby { get; set; }
        public SortDirection sortdirection { get; set; }
    }
}
