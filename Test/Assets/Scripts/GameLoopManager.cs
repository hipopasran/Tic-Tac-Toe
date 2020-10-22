using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : Singleton<GameLoopManager>
{
    [SerializeField] private bool pvp;

    public bool PlayerVsPlayer => pvp;

    public void CreatePVPGame()
    {
        var playerFirst = new GameObject("Player 1");
        var playerFirstInfo = playerFirst.AddComponent<Player>();
        playerFirstInfo.Create("Player 1", CellValueType.xType);
        ReferenceHolder.Instance.PlayerFirst = playerFirst;

        var playerSecond = new GameObject("Player 2");
        var playerSecondInfo = playerSecond.AddComponent<Player>();
        playerSecondInfo.Create("Player 2", CellValueType.oType);
        ReferenceHolder.Instance.PlayerSecond = playerSecond;

        var boardOnLevel = Instantiate(ReferenceHolder.Instance.GameSettings.GetRandomBoard(), Vector3.zero, Quaternion.identity);
        ReferenceHolder.Instance.Board = boardOnLevel;

        GameStateManager.Instance.ChangeState(GameState.PlayerFirstMove);
    }

    public void CreatePVEGame()
    {
        var playerFirst = new GameObject("Player 1");
        var playerFirstInfo = playerFirst.AddComponent<Player>();
        playerFirstInfo.Create("Player 1", CellValueType.xType);
        ReferenceHolder.Instance.PlayerFirst = playerFirst;

        var playerSecond = new GameObject("Player 2");
        var playerSecondInfo = playerSecond.AddComponent<Player>();
        playerSecondInfo.Create("Player 2", CellValueType.oType);
        ReferenceHolder.Instance.PlayerSecond = playerSecond;

        var boardOnLevel = Instantiate(ReferenceHolder.Instance.GameSettings.GetRandomBoard(), Vector3.zero, Quaternion.identity);
        ReferenceHolder.Instance.Board = boardOnLevel;
    }

    public void FinishGame()
    {
        Destroy(ReferenceHolder.Instance.PlayerFirst);
        Destroy(ReferenceHolder.Instance.PlayerSecond);
        Destroy(ReferenceHolder.Instance.Board);

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
}
