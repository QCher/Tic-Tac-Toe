using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CellStateDrawer),typeof(Button))]
[System.Serializable]
public class CellController : MonoBehaviour {

    public  GridController   grid;
    public  Button           button;
    public  CellStateDrawer  drawer;

    public Enums.Row              row;
    public Enums.Column           column;

    void Awake () {
        drawer = (drawer == null) ? GetComponent<CellStateDrawer>() : drawer;
        button = (button == null) ? GetComponent<Button>() : button;
    }

    void Start()
    {
        button.onClick.AddListener(()=> { grid.CellWasPressed(this); });
    }

    public void DrawCellState(Enums.State state)
    {
        drawer.DrawState(state);
    }
}

