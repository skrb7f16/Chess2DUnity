using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public class Board : NetworkBehaviour
{
    public static Board instance;

    public List<List<GameObject>> board;
    public List<Position> positionsToMove;
    public Character selectedCharacter;
    public Position selectedPosition;
    public int chance = 1;
    public bool selectPieceMode;
    public bool pieceSelectedMode;
    [SerializeField] private GameObject[] indicators;
    [SerializeField] private Character[] Kings;


    void Start()
    {
        selectPieceMode = true;
        board = new List<List<GameObject>>();
        Transform[] ts = GetComponentsInChildren<Transform>();
        SetIndicators();
        int i = 0;

        foreach (Transform t in ts)
        {
            if (!t.CompareTag("Rows")) continue;

            board.Add(new List<GameObject>());
            Transform[] ts1 = t.gameObject.GetComponentsInChildren<Transform>();
            int j = 0;
            foreach (Transform t1 in ts1)
            {
                if (!t1.CompareTag("Cols")) continue;
                board[i].Add(t1.gameObject);
                Position temp = t1.gameObject.GetComponent<Position>();
                temp.SetPosition(i, j);
                Character c = t1.gameObject.GetComponentInChildren<Character>();

                if (c != null)
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

    public void SetIndicators()
    {
        indicators[chance].SetActive(true);
        indicators[(chance + 1) % 2].SetActive(false);
    }

    private void LateUpdate()
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


   


    public void ResetBoard()
    {

        SceneManager.LoadScene(1);
    }


    [ServerRpc(RequireOwnership = false)]
    public void HandleClickCharacterServerRPC(int newi, int newj, int oldi, int oldj)
    {

        HandleClickCharacterClientRPC(newi, newj, oldi, oldj);
    }


    [ClientRpc]
    public void HandleClickCharacterClientRPC(int newi, int newj, int oldi, int oldj)
    {
        print("HELLO");
        Position pnew = GetPositionForNextMove(newi, newj);
        Position pold = GetPositionForNextMove(oldi, oldj);
        Character characterMoved = pold.GetCharacter();
        Character characterAlreadyAtNewPosition = pnew.GetCharacter();

        characterMoved.gameObject.transform.parent = pnew.transform;
        characterMoved.transform.localPosition = Vector2.zero;
        if (characterAlreadyAtNewPosition != null)
        {
            Destroy(characterAlreadyAtNewPosition.gameObject);
        }

        pnew.SetHasPiece(true);
        pold.SetHasPiece(false);

        pold.SetCharacter(null);
        pnew.SetCharacter(characterMoved);

        chance = (chance + 1) % 2;
        SetIndicators();
    }

    [ServerRpc(RequireOwnership = false)]
    public void HandleMessageSentServerRPC(string msg, int id)
    {

        HandleMessageRecievedClientRPC(msg, id);

    }

    [ClientRpc]
    public void HandleMessageRecievedClientRPC(string msg, int id)
    {
        UIManager.instance.ShowMessage(msg, id);
    }

}
