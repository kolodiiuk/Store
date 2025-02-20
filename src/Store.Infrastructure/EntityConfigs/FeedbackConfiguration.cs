using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Infrastructure.EntityConfigs;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("feedback");
        
        builder.HasKey(f => f.Id);
        
        builder.HasOne(f => f.Order) 
            .WithMany(o => o.Feedbacks) 
            .HasForeignKey(f => f.OrderId) 
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(f => f.Id)
            .HasColumnName("feedback_id");
        
        builder.Property(f => f.Rating)
            .HasColumnName("rating")
            .IsRequired();
        
        builder.Property(f => f.Comment)
            .HasMaxLength(1000)
            .HasColumnName("comment");
        
        builder.Property(f => f.OrderId)
            .HasColumnName("order_id")
            .IsRequired();
        
        builder.Property(f => f.Created)
            .HasColumnName("created")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("TIMESTAMP");
        
        builder.ToTable(t => t
            .HasCheckConstraint("CK_Feedback_Rating", "Rating >= 1 AND Rating <= 5"));
    }
}
