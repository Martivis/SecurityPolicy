namespace SecurityPolicy;

public class AccessFile
{
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is AccessFile file)
        {
            return file.Name == Name;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}