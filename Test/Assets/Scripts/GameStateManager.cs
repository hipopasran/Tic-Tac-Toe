using System;
using UnityEngine;

public enum GameState
{
    StartMenu,
    ChoseMode,
    PlayerFirstMove,
    MoveProcess,
    PlayerSecondMove,
    EndGame
}

public class GameStateManager : Singleton<GameStateManager>
{
    [SerializeField] private GameState state;

    public GameState State => state;
    public event Action<GameState> OnStateChange;
    public void ChangeState(GameState state)
    {
        this.state = state;
        OnStateChange?.Invoke(this.state);
    }
}
