namespace myCleanArchitecture.Shared.Results
{
    public class ObjectResult<TEntity> : Result
    {
        public ObjectResult(Meta meta) : base(meta)
        {
        }
        public TEntity? Entity { get; set; }
        public ObjectResult(TEntity entity, Meta meta) : base(meta)
        {
            Entity = entity;
        }
    }
}
