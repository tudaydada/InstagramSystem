using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstagramSystem.Entities
{
    [Table("TblPost")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(5000)]
        public string Content { get; set; }

        public string? Type { get; set; }

        public DateTime? CreateAt { get; set; } = default(DateTime?); 

        public DateTime? UpdateAt { get; set; } 

        public string? hagtag { get; set; }

        public int? State { get; set;}

        [MaxLength(1000)]
        public string? FileURL { get; set; }

        public bool IsDelete { get; set; }

        public int UserId { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User Users { get; set; }

        public virtual List<PostComment> PostComments { get; set; }

        public virtual List<PostLike> PostLikes { get; set; }

    }
}
