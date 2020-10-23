using UnityEngine;

public class AIPlayer : Player
{
    [SerializeField] protected IBoard board;

    protected void OnEnable()
    {
        GameStateManager.Instance.OnStateChange += CheckForMove;
    }

    protected void OnDestroy()
    {
        GameStateManager.Instance.OnStateChange -= CheckForMove;
    }

    protected void CheckForMove(GameState state)
    {
        if(state == GameState.PlayerSecondMove)
        {
            Move();
        }
    }
    protected override void Move()
    {
        var board = ReferenceHolder.Instance.Board;
        var emptyCells = board.GetEmptyCells();

        if(emptyCells.Count != 0 )
        {
            var chosenCell = emptyCells[Random.Range(0, emptyCells.Count)];
            chosenCell.Fill(this);
        }
    }
}
