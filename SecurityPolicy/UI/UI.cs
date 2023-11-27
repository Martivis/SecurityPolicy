
using System.Net;
using System.Threading.Channels;

namespace SecurityPolicy.UI;

public class UI
{
    private User? _user;
    private readonly List<User> _users;
    private readonly List<AccessFile> _files;
    private readonly IAccessMatrix _accessMatrix;
    private IMenu _menu;
    private bool _isRun;
    
    public UI(User admin, List<User> users, List<AccessFile> files, IAccessMatrixFactory factory)
    {
        _users = users;
        _files = files;
        _accessMatrix = factory.Create(admin, users, files);
        
        FillMenu();
    }

    private void FillMenu()
    {
        var menu = new List<MenuPoint>()
        {
            new MenuPoint("Read", () => AccessFileAction(Right.Read, "File read successful")),
            new MenuPoint("Write", () => AccessFileAction(Right.Write, "File wrote successful")),
            new MenuPoint("Patch right", PatchRightAction),
            new MenuPoint("Remove right", RemoveRightAction),
            new MenuPoint("Log out", () => _user = null),
            new MenuPoint("Exit", () => _isRun = false)
        };
        _menu = new Menu(menu);
    }

    public void Start()
    {
        _isRun = true;
        while (_isRun)
        {
            if (_user is null)
            {
                IdentifyUser();
            }

            PrintFilesWithRights();

            _menu.Print();
            _menu.ExecuteChoice();
            WaitAnyKey();
        }
    }

    private void IdentifyUser()
    {
        while (_user is null)
        {
            try
            {
                Console.Write("Enter user id >> ");
                var id = int.Parse(Console.ReadLine()!);
                _user = _users.First(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void PrintFilesWithRights()
    {
        if (_user is null)
            return;
        Console.WriteLine("---------------");
        Console.WriteLine($"User {_user.Name}");
        Console.WriteLine("---------------");

        foreach (var file in _files)
        {
            Console.Write($"{file.Name}: ");
            var right = _accessMatrix.GetUserRights(_user, file);
            Console.WriteLine(right);
        }
    }

    private void AccessFileAction(Right right, string successMessage)
    {
        var file = GetChosenFile();

        if (_accessMatrix.CheckRight(_user, file, right))
        {
            PrintColor(successMessage, ConsoleColor.Green);
        }
        else
        {
            PrintColor("Forbidden", ConsoleColor.Red);
        }
    }

    private void PatchRightAction()
    {
        var file = GetChosenFile();
        if (!_accessMatrix.CheckRight(_user, file, Right.Grant))
        {
            PrintColor("Forbidden", ConsoleColor.Red);
            return;
        }
        
        var user = GetChosenUser();
        var right = GetChosenRight();
        
        _accessMatrix.PatchRight(user, file, right);
        PrintColor($"Right {right} on file {file.Name} patched to user {user.Name} successful", ConsoleColor.Green);
    }
    
    private void RemoveRightAction()
    {
        var file = GetChosenFile();
        if (!_accessMatrix.CheckRight(_user, file, Right.Grant))
        {
            PrintColor("Forbidden", ConsoleColor.Red);
            return;
        }
        
        var user = GetChosenUser();
        var right = GetChosenRight();
        
        _accessMatrix.RemoveRight(user, file, right);
        PrintColor($"Right {right} on file {file.Name} patched to user {user.Name} successful", ConsoleColor.Green);
    }

    private static Right GetChosenRight()
    {
        Console.WriteLine("Enter policy name >> ");
        var answer = Console.ReadLine();
        Enum.TryParse(answer, ignoreCase: true, out Right right);
        return right;
    }

    private User GetChosenUser()
    {
        User? user = null;
        while (user is null)
        {
            PrintUsers();
            Console.WriteLine("Enter user id >> ");
            var userAnswer = Console.ReadLine();
            if (!int.TryParse(userAnswer, out var userId))
            {
                PrintColor("Input error...", ConsoleColor.Red);
                continue;
            }
            user = _users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                PrintColor($"User with id {userId} does not exist", ConsoleColor.Red);
            }
        }
        Console.WriteLine("User chosen.");
        return user;
    }

    private AccessFile GetChosenFile()
    {
        AccessFile? file = null;
        while (file is null)
        {
            Console.Write("Enter file name >> ");
            var fileAnswer = Console.ReadLine();
            file = _files.FirstOrDefault(f => f.Name == fileAnswer);
            if (file is null)
            {
                PrintColor($"File with name {fileAnswer} does not exist", ConsoleColor.Red);
            }
        }
        Console.WriteLine("File chosen.");
        return file;
    }

    private void PrintUsers()
    {
        foreach (var user in _users)
        {
            Console.WriteLine(user);
        }
    }

    private static void PrintColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void WaitAnyKey()
    {
        Console.WriteLine("Press any key...");
        Console.ReadLine();
        Console.Clear();
    }
}