using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    CellValueType CellType { get; }
    bool IsFull { get; }
}
