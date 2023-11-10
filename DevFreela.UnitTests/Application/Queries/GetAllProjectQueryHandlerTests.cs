using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            //Arrange
            var projects = new List<Project>
            {
                new Project("Teste 1", "Descrição 1", 1, 2, 10000),
                new Project("Teste 2", "Descrição 2", 1, 2, 20000),
                new Project("Teste 3", "Descrição 3", 1, 2, 30000),
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();

            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectQuery("");

            var getAllProjectsQueryHandler = new GetAllProjectQueryHandler(projectRepositoryMock.Object);

            //Act

            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert

            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once());
        }
    }
}
