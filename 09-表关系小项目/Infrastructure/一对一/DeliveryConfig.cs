using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.一对一;

namespace Infrastructure.一对一
{
    public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("DeliverySet");

            //builder.HasOne<Order>(x => x.Order).WithOne(y => y.Delivery);//.HasForeignKey("ArticleId");
        }
    }
}
