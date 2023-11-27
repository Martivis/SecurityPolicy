namespace SecurityPolicy.UI;

public class MenuPoint
{
    private readonly string _name;
    private readonly Action _action;

    public MenuPoint(string name, Action action)
    {
        _name = name;
        _action = action;
    }

    public override string ToString()
    {
        return _name;
    }

    public void Execute()
    {
        _action.Invoke();
    }
}