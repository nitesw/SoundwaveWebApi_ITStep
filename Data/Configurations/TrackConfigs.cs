using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class TrackConfigs : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasIndex(s => s.Title).IsUnique();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.Property(x => x.IsArchived).HasDefaultValue(false);
        }
    }
}
