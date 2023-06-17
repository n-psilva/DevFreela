using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        /* as operações serão atualizadas no módulo do EF core*/
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration) 
        {
            _dbContext = dbContext;

            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is not null)
            {
                project.Finish();

                _dbContext.SaveChanges();
            }
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is not null)
            {
                project.Start();

                //_dbContext.SaveChanges();
                #region utilizando Dapper
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();

                    var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                    sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, id });
                }
                #endregion
            }
        }
    }
}
