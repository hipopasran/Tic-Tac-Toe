using System;
using System.Collections.Generic;

public interface IBoard
{
    event Action<Player> OnGameEnded;
    IList<Cell> GetEmptyCells();

    void CheckCurrentBoardState(int result, Player player, GameState lastState);
}
