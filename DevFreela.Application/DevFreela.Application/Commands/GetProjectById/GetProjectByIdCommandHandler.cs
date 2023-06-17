using Azure.Core;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.GetProjectById
{
    public class GetProjectByIdCommandHandler : IRequestHandler<GetProjectByIdCommand, object>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetProjectByIdCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetProjectByIdCommand request, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefault(p => p.Id == request.Id);

            var projectDetailViewModel = new GetProjectByIdCommand(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.StartedAt,
                project.FinishedAt,
                project.Client.FullName,
                project.Freelancer.FullName);

            return await Task.FromResult(projectDetailViewModel);
        }
    }
}
