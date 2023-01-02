using System.ComponentModel.DataAnnotations;

namespace InstagramSystem.DTOs
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,12}$", ErrorMessage = "Invalid Password")]
        [MaxLength(128)]
        public string Password { get; set; }
    }
}
