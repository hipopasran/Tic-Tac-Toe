using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
    IPlayer winner { get; }
    IList<Cell> GetEmptyCells();
}
