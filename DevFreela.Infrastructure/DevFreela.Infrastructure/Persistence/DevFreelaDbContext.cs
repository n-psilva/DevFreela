using DevFreela.Core.Entities;
using System.Collections.Generic;

namespace DevFreela.Infrastructure.Persistence
{
    /*
     * será devidamente implementado no módulo do EF Core
     */
    public class DevFreelaDbContext
    {
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
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
