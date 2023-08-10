using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4HW6.Entities;

namespace Module4HW6.EntityConfigurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Phone).IsRequired(false);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.Email).IsRequired(false).HasMaxLength(150);
            builder.Property(p => p.InstagramUrl).IsRequired(false).HasMaxLength(150);
            builder.HasMany(p => p.Songs)
                .WithMany(p => p.Artists)
                .UsingEntity<Dictionary<string, object>>(
                "ArtistSong",
                j => j
                .HasOne<Song>()
                .WithMany()
                .HasForeignKey("SongId")
                .OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasOne<Artist>()
                .WithMany()
                .HasForeignKey("ArtistId")
                .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
