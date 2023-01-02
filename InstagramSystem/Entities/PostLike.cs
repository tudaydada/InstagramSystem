using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstagramSystem.Entities
{
    [Table("TblPostLike")]
    public class PostLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? CreateAt { get; set; } = default(DateTime?);

        public DateTime? UpdateAt { get; set; }

        public bool IsDelete { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }


        //public virtual User Users { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual Post Posts { get; set; }


    }
}
