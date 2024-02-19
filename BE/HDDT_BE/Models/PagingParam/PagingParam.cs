namespace HDDT_BE.Models.PagingParam
{
    public class PagingParam
    {
        public int Start { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; }
        public string? Content { get; set; }
        

        public int Skip
        {
            get
            {
                return (Start > 0 ? Start - 1 : 0) * Limit;
            }
        }
    }
    
    

    public class PagingModel<T>
    {
        public long TotalRows { get; set; }
        
        public IEnumerable<T> Data { get; set; }
    }

    
    
    
}