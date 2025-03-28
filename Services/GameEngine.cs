using System;
using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Data;
using W9_assignment_template.Models;

namespace W9_assignment_template.Services;

public class GameEngine
{
    private readonly GameContext _context;

    public GameEngine(GameContext context)
    {
        _context = context;
    }

    public void DisplayRooms()
    {
        var rooms = _context.Rooms.Include(r => r.Characters).ToList();

        if (!rooms.Any())
        {
            WriteLine("\nNo rooms available.\n", ConsoleColor.Red);
            return;
        }
        else
        {
            foreach (var room in rooms)
            {
                WriteLine($"\nRoom: {room.Name} - {room.Description}", ConsoleColor.DarkMagenta);
                foreach (var character in room.Characters)
                {
                    WriteLine($"\tCharacter: {character.Name}, Level: {character.Level}", ConsoleColor.Magenta);
                }
            }
            Console.WriteLine();
        }
    }

    public void DisplayCharacters()
    {
        var characters = _context.Characters.ToList();
        if (characters.Any())
        {
            WriteLine("\nCharacters:", ConsoleColor.DarkBlue);
            foreach (var character in characters)
            {
                WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}", ConsoleColor.Blue);
            }
            Console.WriteLine();
        }
        else
        {
            WriteLine("\nNo characters available.\n", ConsoleColor.Red);
        }
    }

    public void AddRoom()
    {
        Console.Write("Enter room name: ");
        var name = Console.ReadLine();

        Console.Write("Enter room description: ");
        var description = Console.ReadLine();

        var room = new Room
        {
            Name = name,
            Description = description
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();

        WriteLine($"Room '{name}' added to the game.\n", ConsoleColor.Green);
    }

    public void AddCharacter()
    {
        Console.Write("Enter character name: ");
        var name = Console.ReadLine();

        Console.Write("Enter character level: ");
        var level = int.Parse(Console.ReadLine());

        Console.Write("Enter room ID for the character: ");
        var roomId = int.Parse(Console.ReadLine());

        var room = _context.Rooms.Find(roomId);
        if (room == null)
        {
            WriteLine($"Room not found. {name} not added.\n", ConsoleColor.Red);
            return;
        }

        var character = new Character
        {
            Name = name,
            Level = level,
            RoomId = roomId
        };

        _context.Characters.Add(character);
        _context.SaveChanges();
        WriteLine($"Character {name} added to the {room.Name}.\n", ConsoleColor.Green);
    }

    public void FindCharacter()
    {
        Console.Write("Enter character name: ");
        var name = Console.ReadLine();

        var characters = _context.Characters
            .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
            .ToList();

        if (characters.Any())
        {
            WriteLine("\nCharacter(s) matching your query:", ConsoleColor.DarkYellow);
            foreach (var character in characters)
            {
                WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}", ConsoleColor.Yellow);
            }
            Console.WriteLine();
        }
        else
        {
            WriteLine($"\nNo characters found with the name '{name}'.\n", ConsoleColor.Red);
        }
    }

    public void LevelUpCharacter()
    {
        Console.Write("\nEnter character name: ");
        var name = Console.ReadLine();
        var character = _context.Characters.FirstOrDefault(c => c.Name == name);

        if (character != null)
        {
            Console.Write("Enter new level: ");
            var newLevel = int.Parse(Console.ReadLine());
            character.Level = newLevel;
            _context.SaveChanges();
            WriteLine($"\nCharacter {character.Name} is now level {character.Level}.\n", ConsoleColor.Green);
        }
        else
        {
            WriteLine($"\nCharacter {name} not found.\n", ConsoleColor.Red);
        }
    }

    private void WriteLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}