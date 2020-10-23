using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected string playerName;
    [SerializeField] protected CellValueType cellType;

    public string Name
    {
        get
        {
            return playerName;
        }
        private set
        {
            playerName = value;
        }
    }
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

    public void Create(string name, CellValueType cellType)
    {
        this.Name = name;
        this.gameObject.name = Name;
        CellType = cellType;
    }

    protected abstract void Move();
}
