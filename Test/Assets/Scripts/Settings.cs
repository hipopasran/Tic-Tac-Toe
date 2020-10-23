using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Settings", menuName ="Settings", order = 1)]
public class Settings : ScriptableObject
{
    public string WinString = " Won!";
    public string DrawString = "Draw! No Empty Cell!";
    public GameObject X;
    public GameObject O;
    public List<GameObject> Boards;

    public GameObject GetPrefabByType(CellValueType type)
    {
        if(type == CellValueType.xType)
        {
            return X;
        }
        else
        {
            return O;
        }
    }

    public GameObject GetRandomBoard()
    {
        return Boards[Random.Range(0, Boards.Count)];
    }
}
