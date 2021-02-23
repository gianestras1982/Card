using Microsoft.EntityFrameworkCore;

using Card.Core.Model;

namespace Card.Core.Data
{
    public class CardDBContext : DbContext
    {

        public CardDBContext(DbContextOptions<CardDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Cards>().ToTable("Cards");
            modelBuilder.Entity<Cards>().HasIndex(c => c.CardNumber).IsUnique();


            modelBuilder.Entity<Transaction>().ToTable("Transaction");

        }
    }
}
