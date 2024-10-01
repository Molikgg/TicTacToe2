TicTacToe game = new TicTacToe();
game.Start();
class TicTacToe
{
   public readonly Players Player1;
   public readonly Players Player2;
    Gamelogic Logic;
    public Players CurrentPlayer;
    readonly Boardmap Boardmap;
    public TicTacToe TicTacToe_;

    public TicTacToe()
    {
        Boardmap = new Boardmap();
        Player1 = new Players('X');
        CurrentPlayer = Player1; // To begin with 'X'
        Player2 = new Players('O');
        TicTacToe_ = this;
        Logic = new Gamelogic(TicTacToe_, Boardmap); 
    }

    public void Start()
    {
        Boardmap.BoardLayout(); // To print out the empty Board
        while (PlayerLoop()) ;
    }

    bool PlayerLoop()
    {
        Console.Write($"'{CurrentPlayer.Symbol}' Player Your Turn: ");
        CurrentPlayer.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
        Logic.Gameloop();
        if (Rules.WinningCondition(Boardmap) == true)
        {
            Console.WriteLine(CurrentPlayer.Symbol + " Won");
            return false;
        }
        if (Rules.Draw(Boardmap) == true)
        {
            return false;
        }
        CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        return true;
    }
}
class Players
{
    public int Player { get; set; }
    public char Symbol { get; private set; }

    public Players(char symbol)
    {
        Symbol = symbol;
    }
}
class Gamelogic
{
    readonly private Boardmap Boardmap;
    readonly private TicTacToe TicTacToe;
    public void Gameloop()
    {
        UpdateTile();
        Boardmap.BoardLayout();
    }
    public void UpdateTile()
    {
        for (; ; )
        {
            Rules.Overlap(TicTacToe.CurrentPlayer, Boardmap); //Changes player in every iteration 
            if (TicTacToe.CurrentPlayer.Player == 1) { Boardmap.G = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 2) { Boardmap.H = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 3) { Boardmap.I = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 4) { Boardmap.D = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 5) { Boardmap.E = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 6) { Boardmap.F = TicTacToe.CurrentPlayer.Symbol; return; }

            else if (TicTacToe.CurrentPlayer.Player == 7) { Boardmap.A = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 8) { Boardmap.B = TicTacToe.CurrentPlayer.Symbol; return; }
            else if (TicTacToe.CurrentPlayer.Player == 9) { Boardmap.C = TicTacToe.CurrentPlayer.Symbol; return; }
            else { Rules.NoSpace("Invalid Input", Boardmap, TicTacToe.CurrentPlayer); }
        }
    }
    public Gamelogic(TicTacToe tictactoe, Boardmap boardmap)
    {
        TicTacToe = tictactoe;
        Boardmap = boardmap;
    }
}

static class Rules
{
    public static bool WinningCondition(Boardmap Boardmap)
    {
        if ((Boardmap.A == Boardmap.B && Boardmap.B == Boardmap.C && Boardmap.C != '~') || //Columb static its same for both as one instance is being shared instead of entire member of class 
            (Boardmap.D == Boardmap.E && Boardmap.E == Boardmap.F && Boardmap.F != '~') ||
            (Boardmap.G == Boardmap.H && Boardmap.H == Boardmap.I && Boardmap.I != '~') ||

            (Boardmap.A == Boardmap.D && Boardmap.D == Boardmap.G && Boardmap.G != '~') || //Rows
            (Boardmap.B == Boardmap.E && Boardmap.E == Boardmap.H && Boardmap.H != '~') ||
            (Boardmap.C == Boardmap.F && Boardmap.F == Boardmap.I && Boardmap.I != '~') ||

            (Boardmap.A == Boardmap.E && Boardmap.E == Boardmap.I && Boardmap.I != '~') || //Diagnals
            (Boardmap.C == Boardmap.E && Boardmap.E == Boardmap.G && Boardmap.G != '~'))
        {
            return true;
        }
        return false;
    }
    public static void Overlap(Players Current, Boardmap Boardmap)
    {
        for (; ; )
        {
            if ((Current.Player == 1 && Boardmap.G != '~') || // Current would be different for different instances if in multiple board running simentaniulsy 
                (Current.Player == 2 && Boardmap.H != '~') ||
                (Current.Player == 3 && Boardmap.I != '~') ||
                (Current.Player == 4 && Boardmap.D != '~') ||
                (Current.Player == 5 && Boardmap.E != '~') ||
                (Current.Player == 6 && Boardmap.F != '~') ||
                (Current.Player == 7 && Boardmap.A != '~') ||
                (Current.Player == 8 && Boardmap.B != '~') ||
                (Current.Player == 9 && Boardmap.C != '~'))
            {
                NoSpace("Space Already Occupied!", Boardmap, Current); 
            }
            else { return; }
        }

    }
    public static bool Draw(Boardmap Boardmap)
    {
        if (Boardmap.A != '~' && Boardmap.B != '~' && Boardmap.C != '~' &&
            Boardmap.D != '~' && Boardmap.E != '~' && Boardmap.F != '~' &&
            Boardmap.G != '~' && Boardmap.H != '~' && Boardmap.I != '~')
        {
            Console.WriteLine("The game is Draw!");
            return true; ;
        }
        return false;
    }
    public static void NoSpace(string comment, Boardmap boardmap, Players current)
    {
        boardmap.BoardLayout(); // Causing the previous Board To Show 
        Console.WriteLine(comment);
        Console.WriteLine("Try Again '" + current.Symbol + "' Player");
        current.Player = int.Parse(Console.ReadLine()!);
        Console.Clear();
    }
}

class Boardmap
{

    public char A = '~';
    public char B = '~';
    public char C = '~';
    public char D = '~';
    public char E = '~';
    public char F = '~';
    public char H = '~';
    public char G = '~';
    public char I = '~';

    public void BoardLayout()
    {
        char[,] Tile;
        int row;
        int columb;
        void SymbolColor()
        {
            if (Tile[columb, row] == 'X') { Console.ForegroundColor = ConsoleColor.Green; }

            else if (Tile[columb, row] == 'O') { Console.ForegroundColor = ConsoleColor.Red; }

            else { Console.ForegroundColor = ConsoleColor.White; }
        } // inner method becuase only being used / control by Boardlayout 
        Tile = new char[3, 3]
        {
        { this.A , this.B , this.C },
        { this.D , this.E , this.F },
        { this.G , this.H , this.I },
        };
        {
            for (columb = 0; columb < Tile.GetLength(0); columb++)
            {
                for (row = 0; row < Tile.GetLength(1); row++)
                {
                    SymbolColor();
                    Console.Write(Tile[columb, row]);
                    Console.ForegroundColor = ConsoleColor.White;

                    if (row < Tile.GetLength(1) - 1)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
                if (columb < Tile.GetLength(0) - 1)
                {
                    Console.WriteLine("--+---+--"); // it does this and then leaves line thats why writing console.Writeline Earlier is important 
                }
            }
            Console.WriteLine(); // To leave Space when called
        }
    }
}
