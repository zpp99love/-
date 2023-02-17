using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.数据库2;

namespace Infrastructure.数据库2
{
    public class Order2Config //: IEntityTypeConfiguration<Order2>
    {
        public void Configure(EntityTypeBuilder<Order2> builder)
        {
            builder.ToTable("Order2Set");

            builder.HasOne(x => x.Delivery2).WithOne(y => y.Order2);//.HasForeignKey<Delivery>(y=>y.OrderId);
        }

    }
}
