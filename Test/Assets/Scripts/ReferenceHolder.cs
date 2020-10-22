using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : Singleton<ReferenceHolder>
{
    [SerializeField] private GameObject playerFirst;
    [SerializeField] private GameObject playerSecond;
    [SerializeField] private GameObject board;
    [SerializeField] private Settings settings;

    public GameObject PlayerFirst
    {
        get
        {
            return playerFirst;
        }
        set
        {
            playerFirst = value;
        }
    }

    public GameObject PlayerSecond
    {
        get
        {
            return playerSecond;
        }
        set
        {
            playerSecond = value;
        }
    }

    public GameObject Board
    {
        get
        {
            return board;
        }
        set
        {
            board = value;
        }
    }

    public Settings GameSettings => settings;
}
