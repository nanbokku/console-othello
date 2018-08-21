using System;
using System.Collections;
using System.Collections.Generic;

public class OthelloRuleManager
{
    public Disk[,] Board;
    public int BlackNum { get; private set; }
    public int WhiteNum { get; private set; }

    public Action OnFinished { get; set; }

    private int[,] reverseNum;
    private int passCount = 0;

    public const int Rows = 8;
    public const int Columns = 8;


    public void Init()
    {
        Board = new Disk[Rows, Columns];
        reverseNum = new int[Rows, Columns];

        // Diskの生成、色の初期化
        for (var i = 0; i < Board.GetLength(0); i++)
        {
            for (var j = 0; j < Board.GetLength(1); j++)
            {
                if (i == 3 && j == 3 || i == 4 && j == 4)
                {
                    Board[i, j] = new Disk(Color.White, j, i);
                }
                else if (i == 3 && j == 4 || i == 4 && j == 3)
                {
                    Board[i, j] = new Disk(Color.Black, j, i);
                }
                else
                {
                    Board[i, j] = new Disk(Color.None, j, i);
                }
            }
        }

        WhiteNum = BlackNum = 2;
    }

    public int DiskNum(TurnState turn)
    {
        if (turn.DiskColor == Color.Black)
        {
            return BlackNum;
        }
        else
        {
            return WhiteNum;
        }
    }

    public void FindSquaresToPut(TurnState turn)
    {
        reverseNum = new int[Rows, Columns];

        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        reverseNum[i, j] += CountReverseNum(Board[i, j], x, y, turn);
                    }
                }
            }
        }
    }

    public void CountDisks()
    {
        int whiteNum = 0, blackNum = 0;
        foreach (var square in Board)
        {
            if (square.Color == Color.White)
            {
                whiteNum++;
            }
            else if (square.Color == Color.Black)
            {
                blackNum++;
            }
        }

        if (whiteNum == WhiteNum || blackNum == BlackNum)
        {
            if (++passCount >= 2)
            {
                OnFinished();
            }
            return;
        }

        passCount = 0;
        BlackNum = blackNum;
        WhiteNum = whiteNum;
    }

    public void Put(TurnState turn, int i, int j)
    {
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (CountReverseNum(Board[i, j], x, y, turn) > 0) { Reverse(Board[i, j], x, y, turn); }
            }
        }
        Board[i, j].ChangeColor(turn.DiskColor);
    }

    private void Reverse(Disk disk, int x, int y, TurnState turn)
    {
        int i, j;
        for (i = disk.Position.y + y, j = disk.Position.x + x; ; i += y, j += x)
        {
            if (i < 0 || j < 0 || i >= Rows || j >= Columns) return;
            if (Board[i, j].Color == turn.DiskColor) return;

            Board[i, j].ChangeColor(turn.DiskColor);
        }
    }

    public bool CanPut(int x, int y)
    {
        if (reverseNum[x, y] > 0) return true;

        return false;
    }

    public bool ReverseDiskExists()
    {
        foreach (var num in reverseNum)
        {
            if (num > 0) return true;
        }

        return false;
    }

    private int CountReverseNum(Disk disk, int x, int y, TurnState turn)
    {
        if (Board[disk.Position.y, disk.Position.x].Color != Color.None) return 0;

        int i, j;
        var count = 0;
        for (i = disk.Position.y + y, j = disk.Position.x + x; ; i += y, j += x)
        {
            if (i < 0 || j < 0 || i >= Rows || j >= Columns) return 0;
            if (Board[i, j].Color == Color.None) return 0;

            if (Board[i, j].Color == turn.DiskColor) break;

            count++;
        }

        return count;
    }
}