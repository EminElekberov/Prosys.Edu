using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.EntityConfiguration
{
    public class LessonConfig : IEntityTypeConfiguration<InterView.Domain.Entities.Lesson>
    {
        public void Configure(EntityTypeBuilder<InterView.Domain.Entities.Lesson> builder)
        {
            builder.ToTable(nameof(InterView.Domain.Entities.Lesson), InterView.Domain.Enum.Scheme.General);
            builder.HasKey(entity => entity.Id);
            builder.HasOne(entity => entity.DictionariesClass).WithMany(s => s.Lesson).HasForeignKey(s => s.ClassId).OnDelete(DeleteBehavior.NoAction).IsRequired(true);
            builder.HasOne(entity => entity.Users).WithMany(s => s.Lesson).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired(true);
        }
    }
}
