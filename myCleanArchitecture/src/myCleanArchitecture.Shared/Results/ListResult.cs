namespace myCleanArchitecture.Shared.Results
{
    public class ListResult<TEntity>:Result
    {
        public ListResult(Meta meta) : base(meta)
        {
        }

        public ListResult(Meta meta,List<TEntity>? entities):base(meta)
        {
            Entities = entities;
        }

        public List<TEntity>? Entities{ get; set; }
    }
}
