using SecurityPolicy.Extensions;

namespace SecurityPolicy;

public class AccessMatrixFactory : IAccessMatrixFactory
{
    private User _admin;
    private List<User> _users;
    private List<AccessFile> _files;
    
    private readonly Random _random = new();

    public IAccessMatrix Create(User admin, List<User> users, List<AccessFile> files)
    {
        _admin = admin;
        _users = users;
        _files = files;
        
        var matrix = new AccessMatrix();
        GrantAdmin(matrix);
        FillRandom(matrix);
        return matrix;
    }

    private void GrantAdmin(IAccessMatrix matrix)
    {
        const Right right = Right.Read | Right.Write | Right.Grant;
        foreach (var file in _files)
        {
            matrix.PatchRight(_admin, file, right);
        }
    }
    
    private void FillRandom(IAccessMatrix matrix)
    {
        foreach (var user in _users)
        {
            foreach (var file in _files)
            {
                matrix.PatchRight(user, file, GetRandomRight());
            }
        }
    }

    private Right GetRandomRight()
    {
        var readRight = _random.Next() % 2 != 0;
        var writeRight = _random.Next() % 2 != 0;
        var grantRight = _random.Next() % 2 != 0;

        var right = Right.None;
        if (readRight) right.AddFlag(Right.Read);
        if (writeRight) right.AddFlag(Right.Write);
        if (grantRight) right.AddFlag(Right.Grant);

        return right;
    }
}