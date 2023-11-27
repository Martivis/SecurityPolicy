namespace SecurityPolicy;

public interface IAccessMatrix
{
    void PatchRight(User user, AccessFile file, Right right);
    void RemoveRight(User user, AccessFile file, Right right);
    bool CheckRight(User user, AccessFile file, Right right);
    Right GetUserRights(User user, AccessFile file);
}