using System;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.BoardMessages;
using ProgecDo.Projects;
using ProgecDo.Users;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo
{
    public class ProgecDoDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectManager _projectManager;
        private readonly IRepository<BoardMessage> _boardMessageRepository;

        public ProgecDoDataSeederContributor(ProjectManager projectManager, IProjectRepository projectRepository, IRepository<AppUser, Guid> userRepository,
            IRepository<BoardMessage> boardMessageRepository)
        {
            _projectManager = projectManager;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _boardMessageRepository = boardMessageRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _projectRepository.GetCountAsync() > 0)
            {
                return;
            }

            var firstUser = _userRepository.FirstOrDefault();
            // var creatorId = _userRepository.FirstOrDefault(x => x.UserName == "Admin").Id; // Guid.Parse("740EF43A-6CEB-C0FA-12B3-39F6F03CA23B");
            var myAwesomeProject = await _projectManager.CreateAsync(
                ProjectConsts.FirstProjectTitle,
                firstUser.Id,
                "Hi! This is my awesome project."
            );
            myAwesomeProject.CreationTime = DateTime.Now;

            await _projectRepository.InsertAsync(
                myAwesomeProject
            );

            var boardMessage = await _boardMessageRepository.InsertAsync(
                new BoardMessage(
                    Guid.NewGuid(),
                    BoardMessageConsts.FirstBoardMessageTitle,
                    "Content of the mesage",
                    myAwesomeProject.Id
                ));

            boardMessage.CreatorId = firstUser.Id;
            boardMessage.AddComment(BoardMessageConsts.FirstBoardMessageCommentContent);
            boardMessage.Comments.FirstOrDefault().CreatorId = firstUser.Id;
        }
    }
}