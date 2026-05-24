using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.EntityConfiguration
{
    public class ExamConfig : IEntityTypeConfiguration<InterView.Domain.Entities.Exam>
    {
        public void Configure(EntityTypeBuilder<InterView.Domain.Entities.Exam> builder)
        {
            builder.ToTable(nameof(InterView.Domain.Entities.Exam), InterView.Domain.Enum.Scheme.General);
            builder.HasKey(entity => entity.Id);
            builder.HasOne(entity => entity.Users).WithMany(s => s.Exam).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(entity => entity.Lesson).WithMany(s => s.Exam).HasForeignKey(s => s.LessonId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        }
    }
}
