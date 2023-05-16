using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        /* as operações serão atualizadas no módulo do EF core*/
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdCliente, inputModel.IdFreelancer, inputModel.TotalCost);

            _dbContext.Projects.Add(project);

            _dbContext.SaveChanges();

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.ProjectComments.Add(comment);

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            
            if (project is not null)
            {
                project.Cancel();

                _dbContext.SaveChanges();
            }
            
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

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;
            var projectViewModel = projects
                                   .Select(p=> new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                                   .ToList();
            
            return projectViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefault(p => p.Id == id);

            var projectDetailViewModel = new ProjectDetailsViewModel(
                project.Id, 
                project.Title, 
                project.Description, 
                project.TotalCost, 
                project.StartedAt, 
                project.FinishedAt,
                project.Client.FullName,
                project.Freelancer.FullName);

            return projectDetailViewModel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is not null)
            {
                project.Start();

                _dbContext.SaveChanges();
            }
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            if (project is not null)
            {
                project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

                _dbContext.SaveChanges();
            }
            
        }
    }
}
