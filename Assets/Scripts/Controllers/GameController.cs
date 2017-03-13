using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GameController : MonoBehaviour {


    public static GameController Instance { get; private set; }
    public  AIController.Position   playerLastChoise { get; set; }
    public  GridController          GridController;
    public  bool                    useAI           ;
    private  Enums.State            currentPlayer   = Enums.State.Cross;
    private AIController            AIcontroller     { get; set; }
    private  Enums.State            AIPlayerState  ;
    private  Enums.Complexity       AIComplexity   ;
    [SerializeField]
    private GameObject              FinishMenu;
    [SerializeField]
    private Text                    Message;
    private int[,]                  gameState      
    =   new int[3, 3] 
    { 
        { (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro },
        { (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro },
        { (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro }
    };


    void Awake()
    {
        Instance = this;
        GridController = (GridController == null) ? GetComponent<GridController>() : GridController;
        this.useAI = GameManager.Instance.Settings.useAI;
        if (this.useAI)
        {
            AIPlayerState   =   GameManager.Instance.Settings.AIState;
            AIComplexity    =   GameManager.Instance.Settings.AIMode;
            float randomization = (AIComplexity == Enums.Complexity.Easy) ? 0.18f : 0.25f; // 0.18 та 0.45 підібрані практично
            AIcontroller = (AIcontroller == null) ? new AIController( AIPlayerState, AIComplexity, randomization) : AIcontroller;
        }
    }

    void Start()
    {
        StartGame();
    }

    public void RestartGame()
    {
        gameState = new int[3, 3]
        {
           { (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro },
           { (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro },
           { (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro, (int)Enums.ZerroValue.Zerro }
        };
        GridController.EnableAllCells(true);
        GridController.ResetAllSells();
        currentPlayer = Enums.State.Cross;
        if (this.useAI && AIPlayerState == currentPlayer)
        {
            AIcontroller.MakeChoice(gameState);
        }
    }


    public void SetCell(CellController cell)
    {
        WriteData((int)cell.column, (int)cell.row);
        cell.DrawCellState(currentPlayer);
        if (CheckWinGame())
        {
            OnWinGame(currentPlayer);
            return;
        }
        else if (CheckDrawGame())
        {
            OnDrawGame();
            return;
        }
        ChangePlayer();
        CheckAIDecision();
    }

    private void StartGame()
    {
        if (this.useAI && (this.AIPlayerState == currentPlayer))
        {
            AIcontroller.MakeChoice(gameState);
        }
    }

    private void ChangePlayer()
    {
        currentPlayer = (currentPlayer == Enums.State.Cross) ? Enums.State.Nought : Enums.State.Cross;
       
    }

    private void WriteData(int x, int y) 
    {
        gameState[x, y] = (int)currentPlayer;
        if (currentPlayer != AIPlayerState)
        {
            playerLastChoise = new AIController.Position(x, y);
        }
    }


    private bool CheckWinGame()
    {
        if ((gameState[0, 0] == gameState[0, 1]) &&(gameState[0, 2] == gameState[0, 0]) &&(gameState[0, 0] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(0, 0).drawer.cellView.color = Color.red;
            GridController.GetCellAt(0, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(0, 2).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[1, 0] == gameState[1, 1]) && (gameState[1, 2] == gameState[1, 0]) && (gameState[1, 0] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(1, 0).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 2).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[2, 0] == gameState[2, 1]) && (gameState[2, 2] == gameState[2, 0]) && (gameState[2, 0] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(2, 0).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 2).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[0, 0] == gameState[1, 0]) && (gameState[2, 0] == gameState[0, 0]) && (gameState[0, 0] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(0, 0).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 0).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 0).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[0, 1] == gameState[1, 1]) && (gameState[2, 1] == gameState[1, 1]) && (gameState[0, 1] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(0, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 1).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[0, 2] == gameState[1, 2]) && (gameState[0, 2] == gameState[2, 2]) && (gameState[0, 2] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(0, 2).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 2).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 2).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[0, 2] == gameState[1, 1]) && (gameState[0, 2] == gameState[2, 0]) && (gameState[0, 2] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(0, 2).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 0).drawer.cellView.color = Color.red;
            return true;
        }
        else if ((gameState[0, 0] == gameState[1, 1]) && (gameState[2, 2] == gameState[0, 0]) && (gameState[0, 0] != (int)Enums.ZerroValue.Zerro))
        {
            GridController.GetCellAt(0, 0).drawer.cellView.color = Color.red;
            GridController.GetCellAt(1, 1).drawer.cellView.color = Color.red;
            GridController.GetCellAt(2, 2).drawer.cellView.color = Color.red;
            return true;
        }
        return false;
    }

    private bool CheckDrawGame()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (gameState[i,j] == (int)Enums.ZerroValue.Zerro)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void OnWinGame(Enums.State winner)
    {
        Message.text = ((winner == Enums.State.Cross) ? "X" : "O") + " WINS!!!";
        GridController.EnableAllCells(false);
        ActivateFinishMenu();
    }

    private void OnDrawGame()
    {
        Message.text = "DRAW";
        GridController.EnableAllCells(false);
        ActivateFinishMenu();
    }

    private void CheckAIDecision()
    {
        if (useAI && AIPlayerState == currentPlayer)
        {
            AIcontroller.MakeChoice(gameState);
        }
    }

    private void ActivateFinishMenu()
    {
        FinishMenu.SetActive(true);
    }
}
