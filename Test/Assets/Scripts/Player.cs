using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private CellValueType cellType;

    public string Name { get; private set; }
    public CellValueType CellType
    {
        get
        {
            return cellType;
        }
        private set
        {
            cellType = value;
        }
    }

    public void Create(string Name, CellValueType cellType)
    {
        Name = name;
        this.gameObject.name = Name;
        CellType = cellType;
    }
}
