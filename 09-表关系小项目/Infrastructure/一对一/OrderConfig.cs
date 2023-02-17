using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.一对一;

namespace Infrastructure.一对一
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("OrderSet");

            builder.HasOne(x => x.Delivery).WithOne(y => y.Order).HasForeignKey<Delivery>(y=>y.OrderId);
        }

    }
}
