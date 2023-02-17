using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.多对多;

namespace Infrastructure.多对多
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("StudentSet");

            builder.HasMany<Teacher>(x => x.Teachers).WithMany(y => y.Students)
                    //中间表名称，不指定efcore也会自动建，但是名称不规范
                    //还可以更深入自定义列名称
                    .UsingEntity(n=>n.ToTable("T_Stu_Tea"));
        }
    }
}
