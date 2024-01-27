using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.DAL.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("COURSES")
                .HasMany(c => c.Groups)
                .WithOne(g => g.Course)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(c => c.Name)
                .IsUnique();
    }

}
