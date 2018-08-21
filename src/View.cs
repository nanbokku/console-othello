using System;
using System.Collections;

public class View
{
    public void ShowCurrentBoard(Disk[,] board)
    {
        Console.WriteLine();
        Console.WriteLine("　 0 1 2 3 4 5 6 7");
        Console.WriteLine("　----------------");

        for (var i = 0; i < board.GetLength(0); i++)
        {
            Console.Write(i + "|");
            for (var j = 0; j < board.GetLength(1); j++)
            {
                ShowDisk(board[i, j]);
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("　----------------");
        Console.WriteLine();
    }

    public void ShowResult(TurnState win, TurnState lose)
    {
        Console.WriteLine("Game Finish.");
        Console.WriteLine("Win: " + win.GetType());
        Console.WriteLine("Lose: " + lose.GetType());
    }

    public void ShowInputMessage(TurnState turn)
    {
        Console.WriteLine("This turn is " + turn.GetType() + "(" + turn.DiskColor + ")");
        Console.Write("Input: ");
    }

    private void ShowDisk(Disk disk)
    {
        switch (disk.Color)
        {
            case Color.Black:
                Console.Write("●");
                break;
            case Color.White:
                Console.Write("○");
                break;
            case Color.None:
                Console.Write("　");
                break;
        }
    }
}