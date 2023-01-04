using static System.Console;

Game game = CreateGame(GetMapSize());
game.Run();

ReadLine();

//-----------------------------------------------------------------------------
// Methods
//-----------------------------------------------------------------------------

int GetMapSize()
{
	int mapSize;
	do
	{
		Write("Choose a dungeon size: small, medium, large...");
		string? choice = ReadLine();
		mapSize = choice switch
		{
			"small" => 4,
			"medium" => 6,
			"large" => 8,
			_=> 0
		};

	} while (mapSize == 0);
	return mapSize;
}

Game CreateGame(int size)
{
	Random rand = new();
	
	// Randomly assign Fountain room based on map size.
	Location fountain = new(rand.Next(size), rand.Next(size));
	Location entrance = new(0, 0);

	// Ensure fountain does not start at the entrance.
	while(entrance == fountain)
	{
		fountain = new(rand.Next(size), rand.Next(size));
	}

	Game game = new(size, fountain, entrance);
	return game;
}

//-----------------------------------------------------------------------------
// Class Definitions
//-----------------------------------------------------------------------------

/// <summary>
/// Main game container. Tracks movements, game state.
/// </summary>
public class Game
{
	public Room[,] Map { get; }
	public FountainOfObjects Fountain { get; }
	public Player Player { get; set; }
	public bool GameOver { get; set; } = false;

	public Game(int size, Location fountain, Location entrance)
	{
		Map = new Room[size, size];
		Map[fountain.X, fountain.Y] = Room.Fountain;
		Map[entrance.X, entrance.Y] = Room.Entrance;
		Player = new(entrance);
		Fountain = new(fountain);
	}

	public void Run()
	{
		// Loop over a single turn until game over.
		while (!GameOver)
		{
			DisplayPlayerStatus(Player.Location);
			GetCommand();

			UpdateGameState();
		}
	}

	private void DisplayPlayerStatus(Location playerLocation)
	{
		WriteLine();
		WriteLine("Room " + playerLocation.X + ", " + playerLocation.Y);
		ForegroundColor = ConsoleColor.Magenta;
		WriteLine(GetSenses(playerLocation));
		ForegroundColor = ConsoleColor.Gray;
	}

	private void UpdateGameState()
	{
		if (Fountain.IsActive && GetRoom(Player.Location) == Room.Entrance)
		{
			ForegroundColor = ConsoleColor.Green;
			WriteLine("You have escaped the dungeon after activating the the Fountain" +
				" of Objects!");
			GameOver = true;
		}
	}

	private string GetSenses(Location playerLocation)
	{	
		string sense = string.Empty;
		if (GetRoom(playerLocation) == Room.Entrance)
		{
			sense = "You see light from outside the cavern.";
		}
		else if (GetRoom(playerLocation) == Room.Fountain)
		{
			if (Fountain.IsActive)
			{
				sense = "You hear the rushing water of the fountain.";
			}
			else
			{
				sense = "You hear the steady drip of water from somewhere in the room.";
			}
		}
		return sense;
	}

	private Room GetRoom(Location location)
	{
		return Map[location.X, location.Y];
	}

	private void GetCommand()
	{
		bool validCommand = false;
		while (!validCommand)
		{
			Write("What do you want to do? ");
			string? command = ReadLine();
	
			if (command.ToLower().IndexOf("south") > -1)
			{
				Player.Move(Go.South, Map.GetLength(1));
				validCommand = true;
			}
			else if (command.ToLower().IndexOf("north") > -1)
			{
				Player.Move(Go.North, Map.GetLength(1));
				validCommand = true;
			}
			else if (command.ToLower().IndexOf("west") > -1)
			{
				Player.Move(Go.West, Map.GetLength(0));
				validCommand = true;
			}
			else if (command.ToLower().IndexOf("east") > -1)
			{
				Player.Move(Go.East, Map.GetLength(0));
				validCommand = true;
			}
			else if (command.ToLower().IndexOf("activate") > -1)
			{
				Fountain.ActivateFountain(Player.Location);
				validCommand = true;
			}
			else
			{
				ForegroundColor = ConsoleColor.Yellow;
				WriteLine("I didn't quite catch that.");
				ForegroundColor = ConsoleColor.Gray;

			}
		}
	}
}

/// <summary>
/// User object that tracks it's own location and can move about the dungeon
/// and activate objects.
/// </summary>
public class Player
{
	public Location Location { get; set; }

	// Creates the Player at the starting posotion, usually the entrance.
	public Player(Location location)
	{
		Location = location;
	}

	// Attempt to move the player in the direction commanded.
	public void Move(Go command, int mapSize)
	{
		if (command == Go.North && !OnMapEdge(Edge.North, mapSize))
		{
			Location.Y--;
		}
		else if (command == Go.South && !OnMapEdge(Edge.South, mapSize))
		{
			Location.Y++;
		}
		else if (command == Go.East && !OnMapEdge(Edge.East, mapSize))
		{
			Location.X++;
		}
		else if (command == Go.West && !OnMapEdge(Edge.West, mapSize))
		{
			Location.X--;
		}
		else
		{
			ForegroundColor = ConsoleColor.Red;
			WriteLine("Ouch. Cannot go that way.");
			ForegroundColor = ConsoleColor.Gray;
		}
	}

	private bool OnMapEdge(Edge edge, int mapSize)
	{
		bool onEdge = false;
		if (edge == Edge.North && Location.Y == 0)
		{
			onEdge = true;
		}
		else if (edge == Edge.South && Location.Y == mapSize - 1)
		{
			onEdge = true;
		}
		else if (edge == Edge.West && Location.X == 0)
		{
			onEdge = true;
		}
		else if (edge == Edge.East && Location.X == mapSize - 1)
		{
			onEdge = true;
		}
		return onEdge;
	}
}

/// <summary>
/// The McGuffin of the game. Player must locate and activate the fountain.
/// </summary>
public class FountainOfObjects
{
	public bool IsActive { get; set; } = false;
	public Location Location { get; set; }
	public FountainOfObjects(Location location)
	{
		Location = location;
	}
	public void ActivateFountain(Location playerLocation)
	{
		if (playerLocation == Location)
		{
			ForegroundColor = ConsoleColor.Green;
			WriteLine("You here the dripping rise into a steady rush of water.");
			ForegroundColor = ConsoleColor.Gray;
			IsActive = true;
		}
		else
		{
			ForegroundColor = ConsoleColor.Red;
			WriteLine("You are not in the fountain room.");
			ForegroundColor = ConsoleColor.Gray;
		}
	}
}

/// <summary>
/// Position within the dungeon.
/// </summary>
public record Location
{
	public int X { get; set; }
	public int Y { get; set; }
	public Location(int x, int y)
	{
		X = x;
		Y = y;
	}
}

public enum Room { Empty, Fountain, Entrance}
public enum Go { Nowhere, North, South, East, West }
public enum Edge { North, South, East, West }
