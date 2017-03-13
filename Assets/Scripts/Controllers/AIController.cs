using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AIController  {

    public  Enums.State         AIPlayerState   { get; set; }
    public  float               RandomPercent   { get; set; }
    public  Enums.Complexity    Complexity      { get; set; }

    public AIController( Enums.State aiPlayerState, Enums.Complexity complexity ,float randomPercent)
    {
        this.AIPlayerState  = aiPlayerState;
        this.Complexity     = complexity;
        this.RandomPercent  = randomPercent;
    }

    //********************************************
    public  void MakeChoice(int[,] gameState)
    {
        switch (Complexity)
        {
            case Enums.Complexity.Hard:
                MakeChoiceHard(gameState);
                break;
            case Enums.Complexity.Medium:
                MakeChoiceMedium(gameState);
                break;
            case Enums.Complexity.Easy:
                MakeChoiceEasy(gameState);
                break;
            default:
                break;
        }
    }

    //********************************************
    private void MakeChoiceHard(int[,] gameState)
    {
        var list = GetWinPositions(gameState,AIPlayerState);
        // Спробувати виграти
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Спробувати не програти противнику
        list = GetWinPositions(gameState, (AIPlayerState == Enums.State.Cross)? Enums.State.Nought : Enums.State.Cross);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Спробувати розгалузитись
        list = GetForkPositions(gameState, AIPlayerState);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Заблокувати розгалуження противника 
        list = GetBlockOpponentForkPositions(gameState, AIPlayerState);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Зайняти центр
        if (gameState[1, 1] == (int)Enums.ZerroValue.Zerro)
        {
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(1, 1));
            return;
        }
        // Зайняти найдальший куток
        list = GetCornerAndDistanceSortedPositions(gameState, AIPlayerState);
        if (list.Count > 0)
        {
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(list[0].X, list[0].Y));
            return;
        }
        // Зайняти вільну позицію
        list = GetFreePositions(gameState);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
    }

    private void MakeChoiceMedium(int[,] gameState)
    {
        var list = GetWinPositions(gameState, AIPlayerState);
        // Спробувати виграти
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Спробувати не програти противнику
        list = GetWinPositions(gameState, (AIPlayerState == Enums.State.Cross) ? Enums.State.Nought : Enums.State.Cross);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Спробувати розгалузитись
        list = GetForkPositions(gameState, AIPlayerState);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Зайняти центр
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (gameState[1, 1] == (int)Enums.ZerroValue.Zerro && (random < RandomPercent) )
        {
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(1, 1));
            return;
        }
        //// Зайняти найдальший куток
        list = GetCornerAndDistanceSortedPositions(gameState, AIPlayerState);
        random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (list.Count > 0 && (random < RandomPercent))
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Зайняти вільну позицію
        list = GetFreePositions(gameState);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
    }

    private void MakeChoiceEasy(int[,] gameState)
    {
        var list = GetWinPositions(gameState, AIPlayerState);
        // Спробувати виграти
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Спробувати не програти противнику
        list = GetWinPositions(gameState, (AIPlayerState == Enums.State.Cross) ? Enums.State.Nought : Enums.State.Cross);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        //// Зайняти найдальший куток
        list = GetCornerAndDistanceSortedPositions(gameState, AIPlayerState);
        random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (list.Count > 0 && (random < RandomPercent))
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
        // Зайняти вільну позицію
        list = GetFreePositions(gameState);
        if (list.Count > 0)
        {
            var pos = Position.GetRandom(list);
            GameController.Instance.GridController.CellWasPressed(GameController.Instance.GridController.GetCellAt(pos.X, pos.Y));
            return;
        }
    }

    //********************************************
    private List<Position> GetFreePositions(int[,] gameState)
    {
        List<Position> freePositions = new List<Position>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (gameState[i,j]== (int) Enums.ZerroValue.Zerro)
                {
                    freePositions.Add(new Position(i,j));
                }
            }
        }
        return freePositions;
    }

    private List<Position> GetWinPositions(int[,] gameState, Enums.State playerState)
    {
        List<Position> resultList = new List<Position>();
        List<Position> freePositions = GetFreePositions(gameState);
        foreach (var pos in freePositions)
        {
            Position result = null;
            int[,] dataCopy = new int[3, 3]; 
            System.Array.Copy(gameState, dataCopy, 9);
            dataCopy[pos.X, pos.Y] = (int)playerState;
            result = CheckWin(dataCopy, playerState) ? pos : result;
            if (result!=null)
            {
                resultList.Add(result);
            }
        }
        return resultList;
    }

    private List<Position> GetAttackPositions(int[,] gameState, Enums.State playerState)
    {
        List<Position> resultList = new List<Position>();
        List<Position> freePositions = GetFreePositions(gameState);
        foreach (var pos in freePositions)
        {
            int[,] dataCopy = new int[3, 3];
            System.Array.Copy(gameState, dataCopy, 9);
            dataCopy[pos.X, pos.Y] = (int)playerState;
            List<Position> effectiveList = GetWinPositions(dataCopy, playerState);
            if (effectiveList.Count > 0)
            {
                resultList.Add(pos);
            }
        }
        return resultList;
    }

    private List<Position> GetForkPositions(int[,] gameState, Enums.State playerState)
    {
        List<Position> resultList = new List<Position>();
        List<Position> freePositions = GetFreePositions(gameState);
        foreach (var pos in freePositions)
        {
            int[,] dataCopy = new int[3, 3];
            System.Array.Copy(gameState, dataCopy, 9);
            dataCopy[pos.X, pos.Y] = (int)playerState;
            List<Position> effectiveList = GetWinPositions(dataCopy,playerState);
            if (effectiveList.Count >= 2)
            {
                resultList.Add(pos);
            }
        }
        return resultList;
    }

    private List<Position> GetBlockOpponentForkPositions(int[,] gameState, Enums.State playerState)
    {
        List<Position>  resultList          =   new List<Position>();
        Enums.State     opponentState       =   ( playerState == Enums.State.Cross ) ? Enums.State.Nought : Enums.State.Cross;
        List<Position>  attackList          =   GetAttackPositions( gameState,playerState );
        List<Position>  attackListCopy      =   GetAttackPositions( gameState, playerState );
        List<Position>  opponentForkList    =   GetForkPositions( gameState, opponentState );
        if (opponentForkList.Count == 0)
        {
            //return resultList;            
        }
        else if (attackList.Count == 0)
        {
            resultList = opponentForkList;
        }
        else
        {
            
            for (int i = attackList.Count - 1; i >= 0; i--)
            {
                int[,] dataCopy = new int[3, 3];
                System.Array.Copy(gameState, dataCopy, 9);
                dataCopy[attackList[i].X, attackList[i].Y] = (int)playerState;
                List<Position> tmp = GetWinPositions(dataCopy, playerState);
                foreach (Position pos in opponentForkList)
                {
                    foreach (Position winPos in tmp)
                    {
                        if (winPos == pos)
                        {
                            attackList.RemoveAt(i);
                        }
                    }
                }
            }

            if (attackList.Count == 0)
            {
                Debug.Log("Some error");
                foreach (var pos in opponentForkList)
                {
                    foreach (var attack in attackListCopy)
                    {
                        if (pos == attack)
                        {
                            attackList.Add(pos);
                        }
                    }
                }
            }
            resultList = attackList;
        }
        return resultList;
    }

    private List<Position> GetCornerAndDistanceSortedPositions(int[,] gameState, Enums.State playerState)
    {
        List<Position>  resultList              = new List<Position>();
        List<Position>  freePositions           = GetFreePositions(gameState);
        Position        lastOpponentPosition    = GameController.Instance.playerLastChoise;
        foreach ( Position pos in freePositions )
        {
            if (IsCornerPosition(pos))
            {
                resultList.Add(pos);
            }
        }
        resultList.Sort(new DistanceComparer(lastOpponentPosition));
        return resultList;
    }
    //********************************************
    private bool CheckWin(int[,] gameState, Enums.State state)
    {
        if ((gameState[0, 0] == gameState[0, 1]) && (gameState[0, 2] == gameState[0, 0]) && (gameState[0, 0] == (int)state))
        {
            return true;
        }
        else if ((gameState[1, 0] == gameState[1, 1]) && (gameState[1, 2] == gameState[1, 0]) && (gameState[1, 0] == (int)state))
        {
            return true;
        }
        else if ((gameState[2, 0] == gameState[2, 1]) && (gameState[2, 2] == gameState[2, 0]) && (gameState[2, 0] == (int)state))
        {
            return true;
        }
        else if ((gameState[0, 0] == gameState[1, 0]) && (gameState[2, 0] == gameState[0, 0]) && (gameState[0, 0] == (int)state))
        {
            return true;
        }
        else if ((gameState[0, 1] == gameState[1, 1]) && (gameState[2, 1] == gameState[1, 1]) && (gameState[0, 1] == (int)state))
        {
            return true;
        }
        else if ((gameState[0, 2] == gameState[1, 2]) && (gameState[0, 2] == gameState[2, 2]) && (gameState[0, 2] == (int)state))
        {
            return true;
        }
        else if ((gameState[0, 2] == gameState[1, 1]) && (gameState[0, 2] == gameState[2, 0]) && (gameState[0, 2] == (int)state))
        {
            return true;
        }
        else if ((gameState[0, 0] == gameState[1, 1]) && (gameState[2, 2] == gameState[0, 0]) && (gameState[0, 0] == (int)state))
        {
            return true;
        }
        return false;
    }

    private bool IsCornerPosition(Position position)
    {
        if      ( position.X == 0 && position.Y == 0 )
        {
            return true;
        }
        else if ( position.X == 2 && position.Y == 0 )
        {
            return true;
        }
        else if ( position.X == 0 && position.Y == 2 )
        {
            return true;
        }
        else if ( position.X == 2 && position.Y == 2 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //********************************************
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    public class Position
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        public int X { get; }
        public int Y { get; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position GetRandom(List<Position> list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count - 1);
            return list[randomIndex]; 
        }

        public static bool operator ==(Position a, Position b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }
    }
    //********************************************
    private class DistanceComparer:IComparer<Position>
    {
        private Position zero;
        public Position Zero
        {
            get
            {
                return zero;
            }
            set
            {
                if (value == null)
                {
                    zero = new Position(1, 1);
                }
                else
                {
                    zero = value;
                }
            }
        }
        public DistanceComparer(Position zero)
        {
            Zero = zero;
        }

        public int Compare(Position first, Position second)
        {
            if (first == null)
            {
                if (second == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (second == null)
                {
                    return 1;
                }
                else
                {
                    int firstDist   = Mathf.Abs(first.X - zero.X) + Mathf.Abs(first.Y - zero.Y);
                    int secondDist  = Mathf.Abs(second.X - zero.X) + Mathf.Abs(second.Y - zero.Y);
                    if (firstDist == secondDist)
                    {
                        return 0;
                    }
                    else if (firstDist > secondDist)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }

}
