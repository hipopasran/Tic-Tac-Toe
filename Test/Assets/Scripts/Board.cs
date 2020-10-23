using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Board : MonoBehaviour, IBoard
{
    public event Action<Player> OnGameEnded;

    [SerializeField] private List<Cell> cells;
    public IList<Cell> GetEmptyCells()
    {
        return cells.Where(x => x.IsFull == false).ToList();
    }

    public void CheckCurrentBoardState(int result , Player player, GameState lastState)
    {
        if (result >= 3)
        {
            Win(player);
        }
        else
        {
            if (GetEmptyCells().Count == 0)
            {
                EndEmptyCells();
                GameStateManager.Instance.ChangeState(GameState.EndGame);
            }
            else
            {
                if (lastState == GameState.PlayerFirstMove)
                {
                    GameStateManager.Instance.ChangeState(GameState.PlayerSecondMove);
                }
                else if (lastState == GameState.PlayerSecondMove)
                {
                    GameStateManager.Instance.ChangeState(GameState.PlayerFirstMove);
                }
            }
        }
    }

    private void Win(Player player)
    {
        OnGameEnded?.Invoke(player);
    }

    private void EndEmptyCells()
    {
        Debug.Log("End Cells!");
        OnGameEnded?.Invoke(null);
    }
}
