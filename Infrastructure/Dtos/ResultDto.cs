namespace Infrastructure.Dtos
{
    public enum RequestStatus
    {
        Success,
        Error,
        NotAuthorized,
        NotFound
    }

    public class ResultDto
    {
        public RequestStatus RequestStatus { get; set; }
        public string Message { get; set; }
    }

    public class ResultDto<T> : ResultDto
    {
        public T Data { get; set; }
    }
}
