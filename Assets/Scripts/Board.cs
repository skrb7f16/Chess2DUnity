using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;
    
    public List<List<GameObject>> board;
    public List<Position> positionsToMove;
    public Character selectedCharacter;
    public Position selectedPosition;
    public int chance;
    public bool selectPieceMode;
    public bool pieceSelectedMode;


    void Start()
    {
        selectPieceMode = true;
        board = new List<List<GameObject>>();
        Transform[] ts = GetComponentsInChildren<Transform>();
       
        int i = 0;

        foreach(Transform t in ts)
        {
            if (!t.CompareTag("Rows")) continue;
            
            board.Add(new List<GameObject>());
            Transform[] ts1 = t.gameObject.GetComponentsInChildren<Transform>();
            int j = 0;
            foreach(Transform t1 in ts1)
            {
                if (!t1.CompareTag("Cols")) continue;
                board[i].Add(t1.gameObject);
                Position temp = t1.gameObject.GetComponent<Position>();
                temp.SetPosition(i, j);
                Character c = t1.gameObject.GetComponentInChildren<Character>();
                
                if (c!= null)
                {
                    
                    temp.SetHasPiece(true);
                    temp.SetCharacter(t1.gameObject.GetComponentInChildren<Character>());

                }
                

                j++;
            }
            i++;
        }
        instance = this;
       
    }

    
    void Update()
    {
        
    }

    public Position GetPositionForNextMove(int r,int c)
    {
        
        if (r < 8 && c < 8)
        {
            return board[r][c].GetComponent<Position>();
        }
            
        else return null;
    }



}
