using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    /*
     * será devidamente implementado no módulo do EF Core
     */
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {

        }

        #region DBCONTEXT INMEMORY  
        /*
        public DevFreelaDbContext()
        {
            Projects= new List<Project>
            {
                new Project("meu projeto aspnet core 1", "minha descrição de projeto 1", 1, 1, 10000),
                new Project("meu projeto aspnet core 2", "minha descrição de projeto 2", 1, 1, 20000),
                new Project("meu projeto aspnet core 3", "minha descrição de projeto 3", 1, 1, 20000)
            };

            Users = new List<User>
            {
                new User("Jonatan Silva", "natan@natan.com.br", new DateTime(1990, 6, 15)),
                new User("Dagmar Ribeiro", "dagmar@dagmar.com.br", new DateTime(1989, 8, 9)),
                new User("Leandro Alcunha", "leandro@alcunha.com.br", new DateTime(1982, 2, 20))
            };

            Skills = new List<Skill>
            {
                new Skill(".Net Core"),
                new Skill("C#"),
                new Skill("ASPNET Core")
            };
        }*/
        #endregion

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasKey(p => p.Id);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany(o => o.OwnedProjects)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectComment>().HasKey(p => p.Id);

            modelBuilder.Entity<ProjectComment>()
                .HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdProject)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectComment>()
                .HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Skill>().HasKey(s => s.Id);

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Skills)
                .WithOne()
                .HasForeignKey(u => u.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSkill>().HasKey(us => us.Id);
        }
    }
}
