using System;
using UnityEngine;

public class GameLoopManager : Singleton<GameLoopManager>
{
    public event Action OnResetGame;
    public event Action<Player> OnGameEnd;

    public void CreatePVPGame()
    {
        CreateBoard();

        var playerFirst = CreatePlayer<RealPlayer>("Player 1", CellValueType.xType);
        var playerSecond = CreatePlayer<RealPlayer>("Player 2", CellValueType.oType);
        if (ReferenceHolder.Instance != null) ReferenceHolder.Instance.SetCurrentPlayers(playerFirst, playerSecond);

        GameStateManager.Instance.ChangeState(GameState.PlayerFirstMove);
    }

    public void CreatePVEGame()
    {
        CreateBoard();

        var playerFirst = CreatePlayer<RealPlayer>("Player 1", CellValueType.xType);
        var playerSecond = CreatePlayer<AIPlayer>("AI Player", CellValueType.oType);
        if (ReferenceHolder.Instance != null) ReferenceHolder.Instance.SetCurrentPlayers(playerFirst, playerSecond);

        GameStateManager.Instance.ChangeState(GameState.PlayerFirstMove);
    }

    public void ResetGame()
    {
        OnResetGame?.Invoke();

        StartGame();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameStateManager.Instance.ChangeState(GameState.StartMenu);
    }

    private void GameEnded(Player player)
    {
        OnGameEnd?.Invoke(player);
        GameStateManager.Instance.ChangeState(GameState.EndGame);
    }
    private void CreateBoard()
    {
        var boardOnLevel = Instantiate(ReferenceHolder.Instance.GameSettings.GetRandomBoard(), Vector3.zero, Quaternion.identity);
        if (ReferenceHolder.Instance != null)
        {
            ReferenceHolder.Instance.SetCurrentBoard(boardOnLevel);
            ReferenceHolder.Instance.Board.OnGameEnded += GameEnded;
        }
    }

    private GameObject CreatePlayer<T>(string name, CellValueType cellType) where T : Player
    {
        var player = new GameObject(name);
        var playerInfo = player.AddComponent<T>();
        playerInfo.Create(name, cellType);

        return player;
    }
}
