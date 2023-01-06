using InstagramSystem.Commons;
using InstagramSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstagramSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserFollower> UserFollowers { get; set; }
        public virtual DbSet<UserFeed> UserFeeds { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().Navigation(x => x.Users).AutoInclude();
            modelBuilder.Entity<Post>().Navigation(x => x.PostComments).AutoInclude();
            modelBuilder.Entity<Post>().Navigation(x => x.PostLikes).AutoInclude();

            modelBuilder.Entity<User>().Navigation(x => x.UserFollowers).AutoInclude();
            modelBuilder.Entity<User>().Navigation(x => x.UserRoles).AutoInclude();
            modelBuilder.Entity<User>().Navigation(x => x.Posts).AutoInclude();

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Name = EUserRole.Admin.ToString() },
                new UserRole { Id = 2, Name = EUserRole.User.ToString() } );

            modelBuilder.Entity<Post>()
                .HasMany<PostComment>(post => post.PostComments)
                .WithOne(pm => pm.Posts)
                .HasForeignKey(post => post.PostId);

            modelBuilder.Entity<Post>()
                .HasMany<PostLike>(post => post.PostLikes)
                .WithOne(pm => pm.Posts)
                .HasForeignKey(post => post.PostId);

            //modelBuilder.Entity<TblUser>()
            //    .HasOne<TblUserRole>(user => user.Roles)
            //    .WithMany(role => role.Users)
            //    .HasForeignKey(user => user.Id);

            //modelBuilder.Entity<TblPost>()
            //    .HasMany<TblPostComment>(post => post.Comments)
            //    .WithOne(comment => comment.Posts)
            //    .HasForeignKey(post => post.PostId);

            //modelBuilder.Entity<TblPost>()
            //    .HasMany<TblPostLike>(post => post.Likes)
            //    .WithOne(like => like.Posts)
            //    .HasForeignKey(post => post.PostId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
