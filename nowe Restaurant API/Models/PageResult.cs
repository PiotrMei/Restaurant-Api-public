namespace nowe_Restaurant_API.Models
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int totalpages { get; set; }
        public int itemsfrom { get; set; }
        public int itemsto { get; set; }
        public int totalresults { get; set; }

        public PageResult(List<T> Items, int pagenumber, int pagesize, int totalresults)
        {
            this.Items = Items;
            totalpages = (int) Math.Ceiling(totalresults/(double)pagesize);
            itemsfrom = (pagenumber - 1) * pagesize+1;
            itemsto = itemsfrom + pagesize-1;
            this.totalresults = totalresults;

        }
    }
}
