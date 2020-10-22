using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour, IBoard
{
    private int resultAfterMove;
    [SerializeField] private List<Cell> cells;

    public IPlayer winner { get; private set; }
    public IList<Cell> GetEmptyCells()
    {
        return cells.Where(x => x.IsFull == false).ToList();
    }

    private void Start()
    {
        foreach(var cell in cells)
        {
            cell.OnPlayerWin += Win;
            cell.OnEndOfMove += EndEmptyCells;
        }
    }

    private void Win(IPlayer player)
    {
        winner = player;
        Debug.Log("Winner: " + player);
    }

    private void EndEmptyCells()
    {
        Debug.Log("DRAW! NO EMPTY CELLS");
    }
}
