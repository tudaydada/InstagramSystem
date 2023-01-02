using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstagramSystem.Entities
{
    [Table("TblUserFeed")]
    public class UserFeed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }



        //public virtual Post Posts { get; set; }

        //public virtual User Users { get; set; }

    }
}
