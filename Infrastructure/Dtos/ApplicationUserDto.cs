namespace Infrastructure.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}