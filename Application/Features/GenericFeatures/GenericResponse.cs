namespace Application.Features.GenericFeatures
{
    public sealed class GenericResponse<T>
    {
        public string message { get; set; }
        public T code { get; set; }
        public T result { get; set; }
    }
}
