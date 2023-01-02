namespace InstagramSystem.DTOs
{
    public class ResponseDTO
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
