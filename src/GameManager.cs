using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
    private static GameManager singleton = new GameManager();
    private GameManager() { }
    public static GameManager Instance { get { return singleton; } }

    public bool IsFinished { get; private set; }

    private OthelloRuleManager ruleManager;
    private View view;
    private TurnState currentTurn, nextTurn;

    public static void Main(string[] args)
    {
        Instance.Init();
        Instance.GameStart();

        //Instance.Update();
    }

    public void Init()
    {
        ruleManager = new OthelloRuleManager();
        view = new View();
        currentTurn = new Player(Color.Black);
        nextTurn = new Player(Color.White);

        ruleManager.OnFinished += Finish;
    }

    public void NextTurn()
    {
        currentTurn.Exit(ruleManager);
        view.ShowCurrentBoard(ruleManager.Board);

        var next = currentTurn;
        currentTurn = nextTurn;
        nextTurn = next;

        ruleManager.FindSquaresToPut(currentTurn);
        currentTurn.Enter(ruleManager);
        view.ShowInputMessage(currentTurn);
        currentTurn.Execute(ruleManager);
    }

    public void GameStart()
    {
        ruleManager.Init();
        ruleManager.FindSquaresToPut(currentTurn);

        currentTurn.Enter(ruleManager);
        view.ShowCurrentBoard(ruleManager.Board);
        view.ShowInputMessage(currentTurn);
        currentTurn.Execute(ruleManager);
    }

    public void Update()
    {
        while (true)
        {
            currentTurn.Execute(ruleManager);
        }
    }

    public void Finish()
    {
        IsFinished = true;
        currentTurn = nextTurn = null;

        var firstNum = ruleManager.DiskNum(currentTurn);
        var secondNum = ruleManager.DiskNum(nextTurn);

        if (firstNum > secondNum)
        {
            view.ShowResult(currentTurn, nextTurn);
        }
        else if (firstNum < secondNum)
        {
            view.ShowResult(currentTurn, nextTurn);
        }
    }
}