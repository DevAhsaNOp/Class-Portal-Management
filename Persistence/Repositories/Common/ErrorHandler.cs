namespace Persistence.Repositories.Common
{
    public sealed record ErrorHandler
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}