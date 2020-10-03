using Microsoft.EntityFrameworkCore;
using ProgecDo.Projects;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ProgecDo.EntityFrameworkCore
{
    public static class ProgecDoDbContextModelCreatingExtensions
    {
        public static void ConfigureProgecDo(this ModelBuilder builder, bool isMigrationDbContext)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ProgecDoConsts.DbTablePrefix + "YourEntities", ProgecDoConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Project>(b =>
            {
                b.ToTable(ProgecDoConsts.DbTablePrefix + "Projects", ProgecDoConsts.DbSchema);
                b.Property(x => x.Title).IsRequired().HasMaxLength(ProjectConsts.MaxTitleLength);
                b.Property(x => x.Description);

                b.HasMany(x => x.ProjectUsers)
                    .WithOne(x => x.Project)
                    .HasForeignKey(x => x.ProjectId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                b.ConfigureByConvention(); //auto configure for the base class props
            });

            builder.Entity<ProjectUser>(b =>
            {
                b.ToTable(ProgecDoConsts.DbTablePrefix + "ProjectUsers", ProgecDoConsts.DbSchema);

                b.HasKey(p => new {p.ProjectId, p.UserId});
                // b.HasOne<Project>().WithMany(x=>x.ProjectUsers).HasForeignKey(x => x.ProjectId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                // b.HasOne<AppUser>(x=>x.User).WithOne(x=>x.ProjectUser).HasForeignKey<AppUser>(x=> x.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
                // b.HasOne<AppUser>().WithMany(x=>x.ProjectUsers).HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                if (isMigrationDbContext)
                {
                    b.Ignore(t => t.User);
                }
 
                b.ConfigureByConvention(); //auto configure for the base class props
            });
        }
    }
}