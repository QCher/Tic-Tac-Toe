using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{

    public enum State { Cross = 0, Nought = 1 };
    public enum Row { One = 0, Two = 1, Three = 2 };
    public enum Column { One = 0, Two = 1, Three = 2 };
    public enum GameType {MULTIPLAYER, PLAYERvsAI};
    public enum ButtonType { MULTIPLAYER, PLAYERvsAI, SHOW_AI_SETTINGS , CLOSE_AI_SETTINGS,QUIT };
    public enum Complexity { Hard = 0, Medium = 1, Easy = 2 };
    public enum DropdownType { SET_COMPLEXITY, SET_SIDE};
    public enum ZerroValue { Zerro = 10 };
    public enum FinishMenuButtonType { BACK, REPLAY };
}
