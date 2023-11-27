namespace SecurityPolicy.Extensions;

public static class EnumExtensions
{
    public static bool HasFlag(this Right item, Right flag)
    {
        return (item & flag) != 0;
    }
    
    public static Right AddFlag(this ref Right item, Right flag)
    {
        return item |= flag;
    }
    
    public static Right RemoveFlag(this ref Right item, Right flag)
    {
        return item &= ~flag;
    }
}