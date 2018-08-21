using System;

public abstract class TurnState
{
    public Color DiskColor { get; private set; }

    protected bool active = false;

    public TurnState(Color color)
    {
        DiskColor = color;
    }

    public void Enter(OthelloRuleManager manager)
    {
        active = true;
        if (!manager.ReverseDiskExists())
        {
            GameManager.Instance.NextTurn();
        }
    }

    public void Exit(OthelloRuleManager manager)
    {
        active = false;

        manager.CountDisks();
    }

    public abstract void Execute(OthelloRuleManager manager);
}

public class Player : TurnState
{
    public Player(Color color) : base(color) { }

    public override void Execute(OthelloRuleManager manager)
    {
        if (GameManager.Instance.IsFinished) return;
        if (!active) return;

        int x, y;
        while (true)
        {
            try
            {
                var line = Console.ReadLine();
                var coodinate = line.Split(',');
                if (coodinate.Length != 2)
                {
                    Console.WriteLine("Usage for this format.");
                    Console.WriteLine("Example: 2,3");
                    continue;
                }

                x = Int32.Parse(coodinate[0]);
                y = Int32.Parse(coodinate[1]);

                if (manager.CanPut(x, y)) break;

                Console.WriteLine("You don't put here.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        manager.Put(this, x, y);
        GameManager.Instance.NextTurn();
    }
}