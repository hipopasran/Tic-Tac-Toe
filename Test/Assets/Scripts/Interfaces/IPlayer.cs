using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    string Name { get; }
    CellValueType CellType { get; }

    void Create(string name, CellValueType type);
}
