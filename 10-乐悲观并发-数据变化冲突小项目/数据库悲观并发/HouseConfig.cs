using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace 数据库悲观并发
{
    public class HouseConfig : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            //builder.ToTable("HouseSet");
            builder.Property(x => x.Name).IsRequired();
            
            //乐观并发令牌
            //builder.Property(x=>x.Owner).IsConcurrencyToken();

            //不要和乐观并发一起使用
            builder.Property(x=>x.RowVersion).IsRowVersion();
        }
    }
}
