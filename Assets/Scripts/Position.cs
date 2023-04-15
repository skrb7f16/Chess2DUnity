using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    private int row;
    private int col;
    private bool hasPiece;
    private Character character;
    private bool canBeNextMove;
    private bool enemyToBeKilled;
    public Color colorOfPiece;
    
    void Start()
    {
        colorOfPiece = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        if (canBeNextMove)
        {

        }
    }

    public void SetEnemyToBeKilled(bool b)
    {
        enemyToBeKilled = b;
    }
    public bool GetEnemyToBeKilled()
    {
        return enemyToBeKilled;
    }
    public void SetPosition(int r, int c)
    {
        row = r;
        col = c;
    }

    public int[] GetPosition()
    {
        return new int[] { row, col };
    }

    public void SetHasPiece(bool b)
    {
        hasPiece = b;
    }

    public bool HasPiece()
    {
        return hasPiece;
    }


    public void SetCharacter(Character c)
    {
        character = c;
    }

    public Character GetCharacter()
    {
        return character;
    }
    public void SetCanBeNextMove(bool b)
    {
        canBeNextMove = b;
    }
    public bool GetCanBeNextMove()
    {
        return canBeNextMove;
    }

    private void OnMouseDown()
    {
        
        if (Board.instance.selectPieceMode)
        {
            HandleSelectingPiece();
        }else if (Board.instance.pieceSelectedMode)
        {
            if (canBeNextMove)
            {
                MovePiece();
            }
        }
        
    }


    public void HandleSelectingPiece()
    {
        Board.instance.selectPieceMode = false;
        Board.instance.pieceSelectedMode = true;
        Board.instance.selectedPosition = this;
        Board.instance.selectedCharacter = character;
        Board.instance.positionsToMove = new List<Position>();
        switch (character.GetTypeOfCharacter())
        {
            case Character.TypeOfCharacter.Pawn:
                HandlePawnClicked();
                break;

            case Character.TypeOfCharacter.King:
                HandleKingMovement();
                break;

            case Character.TypeOfCharacter.Knight:
                HandleKnightMovement();
                break;
            default:
                print("hello");
                break;
        }
    }

    public void HandlePawnClicked()
    {
        int[] arr = Moves.GetPawnMove(row, col, character.color);
        Position temp = Board.instance.GetPositionForNextMove(arr[0], arr[1]);
        Board.instance.positionsToMove.Add(temp);
        MarkNextMoves();
    }


    public void HandleKingMovement()
    {
        Moves.GetKingMove(row, col,character.color);
        
        MarkNextMoves();
    }

    public void HandleKnightMovement()
    {
        Moves.GetKnightMove(row,col,character.color);
        MarkNextMoves();
    }

    public void MarkNextMoves()
    {
        foreach (Position t in Board.instance.positionsToMove)
        {
            t.SetCanBeNextMove(true);
            if (!t.GetEnemyToBeKilled())
            {
                t.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                t.GetComponent<SpriteRenderer>().color = Color.red;
            }
            
        }
    }
    public void UnMarkNextMoves()
    {
        foreach (Position t in Board.instance.positionsToMove)
        {
            t.SetCanBeNextMove(false);
            t.GetComponent<SpriteRenderer>().color = t.colorOfPiece;
        }
    }

    public void MovePiece()
    {
        Board.instance.selectedCharacter.transform.parent = transform;
        Board.instance.selectedCharacter.transform.localPosition = Vector2.zero;
        character = Board.instance.selectedPosition.character;
        SetHasPiece(true);
        Board.instance.selectedPosition.character = null;
        Board.instance.selectedPosition.SetHasPiece(false);

        UnMarkNextMoves();
        Board.instance.selectPieceMode = true;
        Board.instance.pieceSelectedMode = false;
        Board.instance.selectedPosition = null;
        Board.instance.selectedCharacter = null;
        


    }
}
