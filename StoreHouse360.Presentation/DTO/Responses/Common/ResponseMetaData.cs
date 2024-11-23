using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Presentation.DTO.Responses.Common
{
    public class ResponseMetaData
    {
        [Required]
        public string Time { get; set; } = DateTime.UtcNow.ToString();
        public string? Message {  get; set; }
    }
}
