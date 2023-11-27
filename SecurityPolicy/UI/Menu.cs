using System.Threading.Channels;

namespace SecurityPolicy.UI;

public class Menu : IMenu
{
    private readonly List<MenuPoint> _menu;

    public Menu(List<MenuPoint> menu)
    {
        _menu = menu;
    }

    public void Print()
    {
        Console.WriteLine("-======- Menu -======-");
        foreach (var menuPoint in _menu)
        {
            Console.WriteLine(menuPoint);
        }
    }

    public void ExecuteChoice()
    {
        Console.Write("Enter action name >> ");
        var answer = Console.ReadLine();
        var result = _menu.FirstOrDefault(p => p.ToString().StartsWith(answer));
        if (result is null)
        {
            Console.WriteLine($"No points with name {answer} were found");
        }
        else
        {
            result.Execute();
        }
    }
}