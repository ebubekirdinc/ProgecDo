using Microsoft.EntityFrameworkCore;
using ProgecDo.BoardMessages;
using ProgecDo.Projects;
using ProgecDo.ToDos;
using ProgecDo.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace ProgecDo.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See ProgecDoMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class ProgecDoDbContext : AbpDbContext<ProgecDoDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside ProgecDoDbContextModelCreatingExtensions.ConfigureProgecDo
         */

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<BoardMessage> BoardMessages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<ToDoItemUser> ToDoItemUsers { get; set; }

        public ProgecDoDbContext(DbContextOptions<ProgecDoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the ProgecDoEfCoreEntityExtensionMappings class
                 */
                b.Property(x => x.ProfileColor).HasMaxLength(AppUserConsts.MaxProfileColorLength);
                b.HasMany(x => x.ProjectUsers)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasMany(x => x.ToDoItemUsers)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /* Configure your own tables/entities inside the ConfigureProgecDo method */

            builder.ConfigureProgecDo(false);
        }
    }
}