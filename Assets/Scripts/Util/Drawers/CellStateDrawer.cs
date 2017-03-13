using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CellStateDrawer : MonoBehaviour {

    public Text cellView;

    public void DrawState(Enums.State state)
    {
        switch (state)
        {
            case Enums.State.Cross:
                DrawCross();
                break;
            case Enums.State.Nought:
                DrawNought();
                break;
            default:
                break;
        }
    }

    public void DrawCross()
    {
        cellView.text = "X";
    }

    public void DrawNought()
    {
        cellView.text = "O";
    }

    public void Clear()
    {
        cellView.text = "";
        cellView.color = Color.black;
    }

}
