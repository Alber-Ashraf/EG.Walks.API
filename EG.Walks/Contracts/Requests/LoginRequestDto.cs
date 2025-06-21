using System.ComponentModel.DataAnnotations;

namespace EG.Walks.Contracts.Requests
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
