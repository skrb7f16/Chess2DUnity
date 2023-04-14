using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;
    
    private List<List<GameObject>> board;

    void Start()
    {
        board = new List<List<GameObject>>();
        Transform[] ts = GetComponentsInChildren<Transform>();
       
        int i = 0;
        foreach(Transform t in ts)
        {
            if (!t.CompareTag("Rows")) continue;
            board.Add(new List<GameObject>());
            Transform[] ts1 = t.gameObject.GetComponentsInChildren<Transform>();
            foreach(Transform t1 in ts1)
            {
                if (!t1.CompareTag("Cols")) continue;
                board[i].Add(t1.gameObject);
                print(t.name+t1.name);
            }
            i++;
        }
        instance = this;
       
    }

    
    void Update()
    {
        
    }

    public GameObject GetPosition(int r,int c)
    {
        if (r < 8 && c < 8)
            return board[r][c];
        else return null;
    }
}
