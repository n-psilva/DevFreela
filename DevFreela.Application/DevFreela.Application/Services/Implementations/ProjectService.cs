using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

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

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.ProjectComments.Add(comment);
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            
            if (project is not null)
            {
                project.Cancel();
            }
            
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is not null)
            {
                project.Finish();
            }
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;
            var projectViewModel = projects
                                   .Select(p=> new ProjectViewModel(p.Title, p.CreatedAt))
                                   .ToList();
            
            return projectViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            var projectDetailViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt);

            return projectDetailViewModel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is not null)
            {
                project.Start();
            }
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            if (project is not null)
            {
                project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
            }
            
        }
    }
}
