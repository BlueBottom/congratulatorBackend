using System.ComponentModel.DataAnnotations;

namespace Congratulator.API.Contracts
{
    // public record BirthdayRequest(
    //     string Name,
    //     string Description,
    //     DateTime Date);

    public class BirthdayRequest
    {
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IFormFile Image { get; set; }
    }
}


