namespace SecurityPolicy;

public interface IAccessMatrixFactory
{
    IAccessMatrix Create(User admin, List<User> users, List<AccessFile> files);
}