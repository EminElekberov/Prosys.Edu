using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.EntityConfiguration
{
    public class UsersConfig : IEntityTypeConfiguration<InterView.Domain.Entities.Users>
    {
        public void Configure(EntityTypeBuilder<InterView.Domain.Entities.Users> builder)
        {
            builder.ToTable(nameof(InterView.Domain.Entities.Users), InterView.Domain.Enum.Scheme.General);
            builder.HasKey(entity => entity.Id);
            builder.HasOne(entity => entity.DictionariesUserType).WithMany(s => s.Users).HasForeignKey(s => s.UserTypeId).OnDelete(DeleteBehavior.NoAction).IsRequired(true);
            builder.HasOne(entity => entity.DictionariesUserClass).WithMany(s => s.UsersClass).HasForeignKey(s => s.UserClassId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        }
    }
}
