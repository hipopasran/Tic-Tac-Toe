﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public enum RawTypeCheck
{
    Vertical, // "|"
    Horizontal, // "-"
    UpLeftRightDiagonal, // "\"
    UpRightLeftDiagonal // "/"
}

public enum CellValueType
{
    None,
    xType,
    oType
}

public class Cell : MonoBehaviour, ICell
{
    public event Action<IPlayer> OnPlayerWin;
    public event Action OnEndOfMove;

    private int result;
    private int tempResult;

    [SerializeField] private Cell upCell;
    [SerializeField] private Cell downCell;
    [SerializeField] private Cell leftCell;
    [SerializeField] private Cell rightCell;
    [SerializeField] private Cell upperLeftCell;
    [SerializeField] private Cell upperRightCell;
    [SerializeField] private Cell downLeftCell;
    [SerializeField] private Cell downRightCell;

    [SerializeField] private Transform valueSpawnPosition;
    [SerializeField] private Board board;

    public CellValueType CellType { get; private set; }
    public bool IsFull { get; private set; }

    public void Fill(IPlayer player)
    {
        var tempState = GameStateManager.Instance.State;
        GameStateManager.Instance.ChangeState(GameState.MoveProcess);
        CellType = player.CellType;
        IsFull = true;

        var value = ReferenceHolder.Instance.GameSettings?.GetPrefabByType(CellType);
        var valueOnScene = Instantiate(value, valueSpawnPosition.position, valueSpawnPosition.rotation);
        valueOnScene.transform.SetParent(valueSpawnPosition);

        tempResult = Check(this, RawTypeCheck.Horizontal);
        result = tempResult > result ? tempResult : result;
        tempResult = Check(this, RawTypeCheck.Vertical);
        result = tempResult > result ? tempResult : result;
        tempResult = Check(this, RawTypeCheck.UpLeftRightDiagonal);
        result = tempResult > result ? tempResult : result;
        tempResult = Check(this, RawTypeCheck.UpRightLeftDiagonal);
        result = tempResult > result ? tempResult : result;

        Debug.Log(result);

        if (result >= 3)
        {
            OnPlayerWin?.Invoke(player);
        }

        if(board.GetEmptyCells().Count == 0)
        {
            GameStateManager.Instance.ChangeState(GameState.EndGame);
        }
        else
        {
            if(tempState == GameState.PlayerFirstMove)
            {
                GameStateManager.Instance.ChangeState(GameState.PlayerSecondMove);
                Debug.Log("Second Player");
            }
            else if(tempState == GameState.PlayerSecondMove)
            {
                GameStateManager.Instance.ChangeState(GameState.PlayerFirstMove);
                Debug.Log("First Player");
            }
        }
    }


    private void OnMouseDown()
    {
        if(IsFull)
        {
            return;
        }

        var currentState = GameStateManager.Instance.State;
        if(currentState == GameState.PlayerFirstMove)
        {
            Fill(ReferenceHolder.Instance.PlayerFirst.GetComponent<IPlayer>());
        }
        else if( currentState == GameState.PlayerSecondMove)
        {
            Fill(ReferenceHolder.Instance.PlayerSecond.GetComponent<IPlayer>());
        }
        else
        {
            return;
        }
    }

    private void OnEnable()
    {
        if(transform.parent != null)
        {
            var tempBoard = transform.GetComponentInParent<Board>();
            if(tempBoard != null)
            {
                board = tempBoard;
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0,1,0) ,out hit, Mathf.Infinity ))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                upCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(1,1,0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                upperRightCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                rightCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(1, -1, 0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                downRightCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                downCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(-1, -1, 0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                downLeftCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                leftCell = tempCell;
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(-1, 1, 0), out hit, Mathf.Infinity))
        {
            var tempCell = hit.collider.GetComponent<Cell>();
            if (tempCell != null)
            {
                upperLeftCell = tempCell;
            }
        }
    }

    public int Check(Cell caller,RawTypeCheck checkType)
    {
        if (caller != null && IsFull && caller.CellType == CellType)
        {

            if (checkType == RawTypeCheck.Horizontal)
            {
                return 1 + TryToCheck(caller, leftCell, RawTypeCheck.Horizontal) + TryToCheck(caller, rightCell, RawTypeCheck.Horizontal);
            }

            if(checkType == RawTypeCheck.Vertical)
            {
                return 1 + TryToCheck(caller, upCell, RawTypeCheck.Vertical) + TryToCheck(caller, downCell, RawTypeCheck.Vertical);
            }

            if(checkType == RawTypeCheck.UpLeftRightDiagonal)
            {
                return 1 + TryToCheck(caller, upperLeftCell, RawTypeCheck.UpLeftRightDiagonal) + TryToCheck(caller, downRightCell, RawTypeCheck.UpLeftRightDiagonal);
            }

            if(checkType == RawTypeCheck.UpRightLeftDiagonal)
            {
                return 1 + TryToCheck(caller, upperRightCell, RawTypeCheck.UpRightLeftDiagonal) + TryToCheck(caller, downLeftCell, RawTypeCheck.UpRightLeftDiagonal);
            }
        }

        return 0;
    }

    private int TryToCheck(Cell caller, Cell cellToCheck, RawTypeCheck checkType)
    {
        if(cellToCheck == null || cellToCheck == caller)
        {
            return 0;
        }

        if(checkType == RawTypeCheck.Horizontal)
        {
            return cellToCheck.Check(this, RawTypeCheck.Horizontal);
        }
        else if(checkType == RawTypeCheck.Vertical)
        {
            return cellToCheck.Check(this, RawTypeCheck.Vertical);
        }
        else if( checkType == RawTypeCheck.UpLeftRightDiagonal)
        {
            return cellToCheck.Check(this, RawTypeCheck.UpLeftRightDiagonal);
        }
        else if( checkType == RawTypeCheck.UpRightLeftDiagonal)
        {
            return cellToCheck.Check(this, RawTypeCheck.UpRightLeftDiagonal);
        }

        return 0;
    }
}