using UnityEngine;

[RequireComponent(typeof(GameController))]
[System.Serializable]
public class GridController : MonoBehaviour {

    [SerializeField]
    private CellController[] cells;

    public void CellWasPressed(CellController cell)
    {
        cell.button.enabled = false;
        GameController.Instance.SetCell(cell);
    }

    public CellController GetCellAt(int i, int j)
    {
        foreach (CellController item in cells)
        {
            if (((int)item.column == i) && ((int)item.row == j))
            {
                return item;
            }
        }
        return null;
    }

    public void EnableAllCells(bool state)
    {
        foreach (var cell in cells)
        {
            cell.button.enabled = state;
        }
    }

    public void ResetAllSells()
    {
        foreach (var cell in cells)
        {
            cell.drawer.Clear();
        }
    }

}
