namespace myCleanArchitecture.Shared.Results
{
    public class PagingResult<TEntity> : Result
    {
        public PagingParameters PagingParameters { get; set; } = new PagingParameters();
        public List<TEntity> Entities { get; set; } = new List<TEntity>();
        public PagingResult() : base(Meta.Success())
        {

        }
    }
}
