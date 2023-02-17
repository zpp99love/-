using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.数据库2;

namespace Infrastructure.数据库2
{
    public class Delivery2Config //: IEntityTypeConfiguration<Delivery2>
    {
        public void Configure(EntityTypeBuilder<Delivery2> builder)
        {
            builder.ToTable("Delivery2Set");

            //builder.HasOne<Order>(x => x.Order).WithOne(y => y.Delivery);//.HasForeignKey("ArticleId");
        }
    }
}
