using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.GetAllProject
{
    public class GetAllProjectCommandHandler : IRequestHandler<GetAllProjectCommand, List<GetAllProjectCommand>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetAllProjectCommand>> Handle(GetAllProjectCommand request, CancellationToken cancellationToken)
        {
            var projects = _dbContext.Projects;
            var listProjects = projects
                                   .Select(p => new GetAllProjectCommand(p.Id, p.Title, p.CreatedAt))
                                   .ToList();

            return await Task.FromResult(listProjects);
        }
    }
}
