using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;
public class Position :NetworkBehaviour ,IPointerDownHandler
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Board.instance.selectPieceMode)
        {
            if (HasPiece() && character.color == Board.instance.chance && Board.instance.chance==(int)NetworkManager.Singleton.LocalClientId)
                HandleSelectingPiece();
        }
        else if (Board.instance.pieceSelectedMode)
        {
            if (canBeNextMove)
            {
                MovePiece();
            }
            else
            {
                Board.instance.selectPieceMode = true;
                Board.instance.pieceSelectedMode = false;
                Board.instance.selectedPosition = null;
                Board.instance.selectedCharacter = null;
                UnMarkNextMoves();
            }
        }
    }


    private void OnMouseDown()
    {
        if (Board.instance.selectPieceMode)
        {
            if (HasPiece() && character.color == Board.instance.chance && Board.instance.chance == (int)NetworkManager.Singleton.LocalClientId)
                HandleSelectingPiece();
        }
        else if (Board.instance.pieceSelectedMode)
        {
            if (canBeNextMove)
            {
                MovePiece();
            }
            else
            {
                Board.instance.selectPieceMode = true;
                Board.instance.pieceSelectedMode = false;
                Board.instance.selectedPosition = null;
                Board.instance.selectedCharacter = null;
                UnMarkNextMoves();
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
            case Character.TypeOfCharacter.Elephant:
                HandleElephantMovement();
                break;
            case Character.TypeOfCharacter.Bishop:
                HandleBishopMovement();
                break;
            case Character.TypeOfCharacter.Queen:
                HandleQueenMovement();
                break;

            default:
                print("Please click on the character");
                
                break;
        }
    }

    public void HandlePawnClicked()
    {
        Moves.GetPawnMove(row, col, character.color);
        
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

    public void HandleElephantMovement()
    {
        Moves.GetElephantMove(row,col,character.color);
        MarkNextMoves();
    }


    public void HandleBishopMovement()
    {
        Moves.GetBishopMove(row,col,character.color);
        MarkNextMoves();
    }

    public void HandleQueenMovement()
    {
        Moves.GetQueenMove(row, col, character.color);
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
            t.SetEnemyToBeKilled(false);
            t.GetComponent<SpriteRenderer>().color = t.colorOfPiece;
        }
    }

    public void MovePiece()
    {
        UnMarkNextMoves();
        Board.instance.selectPieceMode = true;
        Board.instance.pieceSelectedMode = false;

        Board.instance.HandleClickCharacterServerRPC(row, col, Board.instance.selectedPosition.row, Board.instance.selectedPosition.col);
        /*
        Board.instance.selectedCharacter.transform.parent = transform;
        Board.instance.selectedCharacter.transform.localPosition = Vector2.zero;
        if (character != null)
        {
            if (character.GetTypeOfCharacter() == Character.TypeOfCharacter.King)
            {
                Board.instance.ResetBoard();
            }
            Destroy(character.gameObject);
        }
        character = Board.instance.selectedPosition.character;
        SetHasPiece(true);
        Board.instance.selectedPosition.character = null;
        Board.instance.selectedPosition.SetHasPiece(false);

        UnMarkNextMoves();
        Board.instance.selectPieceMode = true;
        Board.instance.pieceSelectedMode = false;
        Board.instance.selectedPosition = null;
        Board.instance.selectedCharacter = null;
        Board.instance.chance = (Board.instance.chance + 1) % 2;
        Board.instance.SetIndicators();
        */
    }

    
}
