using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence.Configurations
{
    public class DespatchConfiuration : IEntityTypeConfiguration<Despatch>
    {
        public void Configure(EntityTypeBuilder<Despatch> builder)
        {
            builder.Property(b => b.IndentingDept)
               .IsRequired();


            builder.Property(b => b.ReferenceNo)
                .IsRequired()
                .HasMaxLength(250);
            builder
            .HasIndex(b => b.ReferenceNo)
            .IsUnique();

            builder.Property(b => b.Purpose)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
