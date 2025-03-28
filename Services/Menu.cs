namespace W9_assignment_template.Services;

public class Menu
{
    private readonly GameEngine _gameEngine;

    public Menu(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("1. Display Rooms");
            Console.WriteLine("2. Display Characters");
            Console.WriteLine("3. Add a Room");
            Console.WriteLine("4. Add a Character");
            Console.WriteLine("5. Find a Character");
            Console.WriteLine("6. Level Up a Character");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _gameEngine.DisplayRooms();
                    break;
                case "2":
                    _gameEngine.DisplayCharacters();
                    break;
                case "3":
                    _gameEngine.AddRoom();
                    break;
                case "4":
                    _gameEngine.AddCharacter();
                    break;
                case "5":
                    _gameEngine.FindCharacter();
                    break;
                case "6":
                    _gameEngine.LevelUpCharacter();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

}