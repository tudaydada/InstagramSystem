using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstagramSystem.Entities
{
    [Table("TblUser")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,12}$", ErrorMessage = "Invalid Password")]
        [MaxLength(128)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public int? Sex { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(1024)]
        public string? ImageURL { get; set; }

        public bool IsDelete { get; set; }

        public int RoleId { get; set; }


        [ForeignKey(nameof(RoleId))]
        public virtual UserRole UserRoles { get; set; }

        public virtual List<UserFollower> UserFollowers { get; set; }

        public virtual List<Post> Posts { get; set; }

    }
}
