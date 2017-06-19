using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using TestTask.Shortener.DAL.Entities;

namespace TestTask.Shortener.DAL
{
    public class ShortenerContext : DbContext
    {
        public ShortenerContext()
           : base("shortener")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLink> UserLinks { get; set; }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.ShortenerUserId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            modelBuilder.Entity<UserLink>()
                .Property(u => u.ShortLink)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

        }
    }
}
