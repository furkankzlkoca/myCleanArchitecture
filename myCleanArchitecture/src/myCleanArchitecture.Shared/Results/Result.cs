namespace myCleanArchitecture.Shared.Results
{
    public class Result
    {
        public Result(Meta meta)
        {
            Meta = meta;
        }

        public Meta Meta { get; set; }
    }
}
