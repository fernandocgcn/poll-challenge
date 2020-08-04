using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollChallenge.Model.Entities;

namespace PollChallenge.Model.Mappings
{
    internal class OptionMap : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("tb_option");

            // columns
            builder.Property(entity => entity.Id)
                .HasColumnName("option_id")
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(entity => entity.Description)
                .HasColumnName("description")
                .IsRequired();
            builder.Property(entity => entity.VotesQty)
                .HasColumnName("votes_qty")
                .ValueGeneratedNever()
                .HasDefaultValue(0)
                .IsRequired();
            builder.Property<int>("poll_id")
                .ValueGeneratedNever()
                .IsRequired();

            // primary key
            builder.HasKey("poll_id", nameof(Option.Id));

            // relationship
            builder.HasOne(option => option.Poll)
                .WithMany(poll => poll.Options)
                .HasForeignKey("poll_id")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
