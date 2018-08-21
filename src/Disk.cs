using System;
using System.Collections;
using System.Collections.Generic;

public class Disk
{
    public Color Color { get; private set; }
    public Position Position { get; private set; }

    public Disk(Color color, int x, int y)
    {
        this.Color = color;
        Position = new Position(x, y);
    }

    public void ChangeColor(Color color)
    {
        this.Color = color;
    }
}

public enum Color
{
    Black,
    White,
    None
}

public struct Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}