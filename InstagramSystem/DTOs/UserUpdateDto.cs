using InstagramSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstagramSystem.DTOs
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public int? Sex { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

    }
}
