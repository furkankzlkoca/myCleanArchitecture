namespace myCleanArchitecture.Application.Common.Results
{
    public class PagingParameters
    {
        public PagingParameters()
        {
            IsDesc= true;
            Page = 1;
            PageSize = 10;
            OrderBy= "Id";
        }

        public PagingParameters(string orderBy)
        {
            OrderBy = orderBy;
        }

        public int Page{ get; set; }
        public int PageSize { get; set; }
        public string OrderBy{ get; set; }
        public bool IsDesc{ get; set; }

        public int? TotalCount { get; set; }
        public int? CurrentPageSize{ get; set; }
        public int? TotalPages{ get; set; }
    }
}
