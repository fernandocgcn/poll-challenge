using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollChallenge.Model.Entities;

namespace PollChallenge.Model.Mappings
{
    internal class PollMap : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.ToTable("tb_poll");

            // columns
            builder.Property(entity => entity.Id)
                .HasColumnName("poll_id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(entity => entity.Description)
                .HasColumnName("description")
                .IsRequired();
            builder.Property(entity => entity.ViewsQty)
                .HasColumnName("views_qty")
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .IsRequired();

            // primary key
            builder.HasKey(entity => entity.Id);
        }
    }
}
