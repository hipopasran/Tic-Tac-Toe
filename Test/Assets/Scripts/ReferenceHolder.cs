using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : Singleton<ReferenceHolder>
{
    [SerializeField] private string lastWinner;
    [SerializeField] private GameObject playerFirst;
    [SerializeField] private GameObject playerSecond;
    [SerializeField] private GameObject board;
    [SerializeField] private Settings settings;

    public Player PlayerFirst { get; private set; }

    public Player PlayerSecond { get; private set; }

    
    public IBoard Board { get; private set; }
    public Settings GameSettings => settings;

    public string LastWinner
    {
        get
        {
            return lastWinner;
        }
        set
        {
            lastWinner = value;
        }
    }

    public void SetCurrentBoard(GameObject board)
    {
        this.board = board;
        if (board != null)
        {
            Board = board.GetComponent<IBoard>();
        }
    }

    public void SetCurrentPlayers(GameObject playerFirst, GameObject playerSecond)
    {
        this.playerFirst = playerFirst;
        this.playerSecond = playerSecond;

        if (playerFirst != null)
        {
            PlayerFirst = playerFirst.GetComponent<Player>();
        }
        if (playerSecond != null)
        {
            PlayerSecond = playerSecond.GetComponent<Player>();
        }
    }

    public void Reset()
    {
        Destroy(playerFirst);
        Destroy(playerSecond);
        Destroy(board);
    }

    private void OnEnable()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.OnResetGame += Reset;
        }
    }
}
