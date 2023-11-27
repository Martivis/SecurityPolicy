namespace SecurityPolicy;

public class User
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    
    public override bool Equals(object? obj)
    {
        if (obj is User user)
        {
            return user.Id == Id;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}