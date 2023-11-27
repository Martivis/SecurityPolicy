using SecurityPolicy.Extensions;

namespace SecurityPolicy;

public class AccessMatrix : IAccessMatrix
{
    private readonly Dictionary<(User, AccessFile), Right> _rights;

    public AccessMatrix()
    {
        _rights = new Dictionary<(User, AccessFile), Right>();
    }

    public void PatchRight(User user, AccessFile file, Right right)
    {
        if (_rights.TryGetValue((user, file), out var current))
        {
            _rights[(user, file)] = current.AddFlag(right);
        }
        else
        {
            _rights.Add((user, file), right);
        }
    }
    
    public void RemoveRight(User user, AccessFile file, Right right)
    {
        if (_rights.TryGetValue((user, file), out var current))
        {
            _rights[(user, file)] = current.RemoveFlag(right);
        }
    }

    public bool CheckRight(User user, AccessFile file, Right right)
    {
        return _rights[(user, file)].HasFlag(right);
    }

    public Right GetUserRights(User user, AccessFile file)
    {
        return _rights[(user, file)];
    }
}