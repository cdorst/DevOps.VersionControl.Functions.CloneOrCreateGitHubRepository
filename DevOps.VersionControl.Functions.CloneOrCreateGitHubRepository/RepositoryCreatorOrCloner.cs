using Common.Structures.HttpBasicAuthentication;
using DevOps.VersionControl.Structures.GitCommitUser;
using DevOps.VersionControl.Structures.GitHub;
using System.Threading.Tasks;
using static Common.Functions.ClearDirectory.DirectoryClearer;
using static DevOps.VersionControl.Functions.CheckIfGitHubRepositoryExists.GitHubRepositoryExistenceChecker;
using static DevOps.VersionControl.Functions.CreateAndInitDotNetGitHubRepository.RepositoryCreator;
using static DevOps.VersionControl.Functions.GetGitHubRepoRemoteUri.RemoteUriGetter;
using static DevOps.VersionControl.Functions.RunGitCloneCommand.GitCloneCommandRunner;
using static Metaproject.Users.CDorst.GitHubAccessToken.GitHubCredentials;

namespace DevOps.VersionControl.Functions.CloneOrCreateGitHubRepository
{
    public static class RepositoryCreatorOrCloner
    {
        public static async Task CloneOrCreate(string directory, string name, string description, BasicAuthenticationCredentials credentials = null, UserInfo? user = null)
        {
            credentials = credentials ?? CDorst;
            var repository = new AccountRepository(credentials.User, name);
            Clear(directory);
            if (await Exists(repository)) Clone(directory, RemoteUri(repository).ToString());
            else await CreateRepository(directory, name, description, credentials, user);
        }
    }
}
