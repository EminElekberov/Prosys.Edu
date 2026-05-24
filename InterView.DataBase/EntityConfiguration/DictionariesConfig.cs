using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.EntityConfiguration
{
    public class DictionariesConfig : IEntityTypeConfiguration<InterView.Domain.Entities.Dictionaries>
    {
        public void Configure(EntityTypeBuilder<InterView.Domain.Entities.Dictionaries> builder)
        {
            builder.ToTable(nameof(InterView.Domain.Entities.Dictionaries), InterView.Domain.Enum.Scheme.General);
            builder.HasKey(entity => entity.Id);
            builder.HasOne(entity => entity.ParentType).WithMany(s => s.DictionariesChilds).HasForeignKey(s => s.ParentId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);

        }
    }
}
