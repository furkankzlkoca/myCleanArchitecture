namespace myCleanArchitecture.Application.Common.Results
{
    public class PagingResult<TEntity> : Result
    {
        public PagingParameters PagingParameters { get; set; }
        public List<TEntity> Entities { get; set; }
        public PagingResult() : base(Meta.Success())
        {

        }
    }
}
