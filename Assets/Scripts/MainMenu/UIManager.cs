using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject board;
    [SerializeField] private TextMeshProUGUI msgRecieved;
    [SerializeField] private TMP_InputField msgToBeSent;
    [SerializeField] private Button host;
    [SerializeField] private Button client;
    [SerializeField] private RawImage sendBtn;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        print("HELLO");
        Application.Quit();
    }


    public void Host()
    {
        board.SetActive(true);
        sendBtn.gameObject.SetActive(true);
        msgToBeSent.gameObject.SetActive(true);
        host.gameObject.SetActive(false);
        client.gameObject.SetActive(false);
        NetworkManager.Singleton.StartHost();
        
    }
    public void Client()
    {
        board.SetActive(true);
        sendBtn.gameObject.SetActive(true);
        msgToBeSent.gameObject.SetActive(true);
        host.gameObject.SetActive(false);
        client.gameObject.SetActive(false);
        NetworkManager.Singleton.StartClient();
        
    }


    public void SendMessage()
    {
        //msgRecieved.text +="You: " +msgToBeSent.text+"\n" ;
        Board.instance.HandleMessageSentServerRPC(msgToBeSent.text, (int)NetworkManager.Singleton.LocalClientId);
        msgToBeSent.text = "";
    }

    public void ShowMessage(string msg, int id)
    {

        if ((int)NetworkManager.Singleton.LocalClientId != id)
            msgRecieved.text += "Other: " + msg + "\n";
        else msgRecieved.text += "You: " + msg + "\n";
    }
}
