namespace myCleanArchitecture.Application.Common.Results
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
