using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Models;

namespace W9_assignment_template.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=bitsql.wctc.edu;Initial Catalog=EAL_20023_W9;User ID=elusz;Password=000538804");
        //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StartingEFCore;Trusted_Connection=True;");
    }

    // Seed Method
    public void Seed()
    {
        if (!Rooms.Any())
        {
            var room1 = new Room { Name = "Entrance Hall", Description = "The main entry." };
            var room2 = new Room { Name = "Treasure Room", Description = "A room filled with treasures." };

            var character1 = new Character { Name = "Knight", Level = 1, Room = room1 };
            var character2 = new Character { Name = "Wizard", Level = 2, Room = room2 };

            Rooms.AddRange(room1, room2);
            Characters.AddRange(character1, character2);

            SaveChanges();
        }
    }
}