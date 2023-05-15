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
    }
}
