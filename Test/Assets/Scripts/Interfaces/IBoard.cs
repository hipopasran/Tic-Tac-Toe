using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
    event Action<Player> OnGameEnded;
    IList<Cell> GetEmptyCells();

    void CheckCurrentBoardState(int result, Player player, GameState lastState);
}
