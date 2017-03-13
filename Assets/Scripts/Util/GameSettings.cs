using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    public bool             useAI   { get; set; }
    public Enums.Complexity AIMode  = Enums.Complexity.Hard;
    public Enums.State      AIState = Enums.State.Nought;
}
