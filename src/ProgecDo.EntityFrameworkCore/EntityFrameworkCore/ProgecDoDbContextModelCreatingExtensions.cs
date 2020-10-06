using System.Data;
using Microsoft.EntityFrameworkCore;
using ProgecDo.BoardMessages;
using ProgecDo.Projects;
using ProgecDo.ToDos;
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

            builder.Entity<BoardMessage>(b =>
            {
                b.ToTable(ProgecDoConsts.DbTablePrefix + "BoardMessages", ProgecDoConsts.DbSchema);

                b.Property(x => x.Title).IsRequired().HasMaxLength(BoardMessageConsts.MaxTitleLength);
                b.Property(x => x.Content).IsRequired().HasMaxLength(BoardMessageConsts.MaxContentLength);
                b.Property(x => x.ProjectId).IsRequired();
                b.HasMany(x => x.Comments)
                    .WithOne(x => x.BoardMessage)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                b.ConfigureByConvention();
            });

            builder.Entity<Comment>(b =>
            {
                b.ToTable(ProgecDoConsts.DbTablePrefix + "Comments", ProgecDoConsts.DbSchema);

                b.Property(x => x.Content).IsRequired().HasMaxLength(BoardMessageConsts.MaxContentLength);
                b.Property(x => x.ParentId).IsRequired();

                b.ConfigureByConvention();
            });

            builder.Entity<ToDo>(b =>
            {
                b.ToTable(ProgecDoConsts.DbTablePrefix + "ToDos", ProgecDoConsts.DbSchema);

                b.Property(x => x.Name).IsRequired().HasMaxLength(ToDoConsts.MaxNameLength);
                b.Property(x => x.Description).IsRequired().HasMaxLength(ToDoConsts.MaxDescriptionLength);
                b.Property(x => x.ProjectId).IsRequired();
                b.HasMany(x => x.ToDoItems)
                    .WithOne(x => x.ToDo)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                b.ConfigureByConvention();
            });

            builder.Entity<ToDoItem>(b =>
            {
                b.ToTable(ProgecDoConsts.DbTablePrefix + "ToDoItems", ProgecDoConsts.DbSchema);

                b.Property(x => x.Description).IsRequired().HasMaxLength(ToDoConsts.MaxToDoItemDescriptionLength);
                b.Property(x => x.ParentId).IsRequired();
                b.Property(x => x.DueDate).HasColumnType("Date");

                b.ConfigureByConvention();
            });
        }
    }
}