
class TicTacToe
{
    static public void Main()
    {
        var game = new Game();
        var board = new Board();
        var player1 = new Player(1);
        var player2 = new Player(2);
        bool gameOver = false;

        Console.WriteLine("7|8|9");
        Console.WriteLine("4|5|6");
        Console.WriteLine("1|2|3");

        while (!gameOver)
        {
            Console.WriteLine($"It is Player {game.GetActivePlayer()}'s turn.");
            board.DisplayBoard();
            game.GetActivePlayerChoice(player1, player2);
            board.UpdateBoard(game.Turn, player1, player2);
            gameOver = board.CheckState(game.Turn);
            game.Turn++;
            Console.WriteLine();
        }
    }
}
  


public class Player
{
    public int Id { get; }
    public int Choice { get; set; }

    public Player(int id)
    {
        Id = id;
    }

    public void SetPlayerChoice()
    {
        bool set = false;
        while (!set)
        {
            Console.WriteLine("What square would you like to play?:  ");


            string? choice = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(choice, out int square) && 
                square > 0 &&
                square < 10)
            {
                set = true;
                Choice = square - 1;
            }
            else
            {
                Console.WriteLine("That choice is not valid. Choose again.");
            }
        }
        
    }
}
    
public class Board
{
    public char[] Spaces { get; set; } = new char[9];

    public Board()
    {
        for (int i = 0; i < 9; i++)
        {
            Spaces[i] = ' ';
        }
    }

    
    public void DisplayBoard()
    {
        Console.WriteLine($" {Spaces[6]} | {Spaces[7]} | {Spaces[8]}");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {Spaces[3]} | {Spaces[4]} | {Spaces[5]}");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {Spaces[0]} | {Spaces[1]} | {Spaces[2]}");
    }

    public void UpdateBoard(int turn, Player p1, Player p2)
    {
        if (turn % 2 != 0)
        {
            Spaces[p1.Choice] = 'X';
        }
        else
        {
            Spaces[p2.Choice] = 'O';
        }
    }

    public bool CheckState(int turn)
    {
        bool endGame = false;
        if ((Spaces[6] != ' ' && Spaces[6] == Spaces[7] && Spaces[7] == Spaces[8]) ||
            (Spaces[3] != ' ' && Spaces[3] == Spaces[4] && Spaces[4] == Spaces[5]) ||
            (Spaces[0] != ' ' && Spaces[0] == Spaces[1] && Spaces[1] == Spaces[2]) ||
            (Spaces[6] != ' ' && Spaces[6] == Spaces[3] && Spaces[3] == Spaces[0]) ||
            (Spaces[7] != ' ' && Spaces[7] == Spaces[4] && Spaces[4] == Spaces[1]) ||
            (Spaces[8] != ' ' && Spaces[8] == Spaces[5] && Spaces[5] == Spaces[2]) ||
            (Spaces[6] != ' ' && Spaces[6] == Spaces[4] && Spaces[4] == Spaces[2]) ||
            (Spaces[8] != ' ' && Spaces[8] == Spaces[4] && Spaces[4] == Spaces[0]))
        {
            if(turn % 2 != 0)
            {
                Console.WriteLine("Player 1 WINS");
            }
            else
            {
                Console.WriteLine("Player 2 WINS");
            }
            endGame = true;
        }
        else if (turn == 9)
        {
            endGame = true;
            Console.WriteLine("TIE GAME");
        }
        return endGame;
    }
}

public class Game
{
    public int Turn { get; set; } = 1;

    public int GetActivePlayer()
    {
        if (Turn % 2 == 0) return 2;
        else return 1;
    }

    public void GetActivePlayerChoice(Player p1, Player p2)
    {
        if (Turn % 2 != 0)
        {
            p1.SetPlayerChoice();
        }
        else
        {
            p2.SetPlayerChoice();
        }
    }
}

