using ManageBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ManageBook.Data
{
    public class ManageBooksDbContext : IdentityDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ManageBooksDbContext(DbContextOptions<ManageBooksDbContext> options) : base(options) {}

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                        .HasMany<Book>()
                        .WithOne(e => e.Author)
                        .HasForeignKey(e => e.AuthorId)
                        .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
