using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : Photon.MonoBehaviour {

    public RectTransform myPanel;
    public GameObject myTextPrefab;
    List<string> chatEvents;
    private float nextMessage;
    private int myNumber = 0;
    private GameObject newText;

    public InputField chatInputField;
    private string chatMessageSent = "";
    private string chatMessageRecieved = "";
    private bool messageSent = true;

    // Use this for initialization
    void Start () {
        myPanel = ChatControl.control.myPanel;
        chatInputField = ChatControl.control.chatInputField;
        chatEvents = new List<string>();
    }
	
	// Update is called once per frame
	void Update () {
        if (chatInputField.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            chatEvents.Add(chatInputField.text);
            messageSent = false; //new message has not been set
            
            chatMessageSent = "  " + NetworkManager.control.GetDisplayName() + ": " + chatInputField.text;

            GameObject newText = (GameObject)Instantiate(myTextPrefab);
            newText.transform.SetParent(myPanel);
            newText.transform.localPosition = new Vector3(newText.transform.localPosition.x, newText.transform.localPosition.y, 1);
            newText.transform.localScale = new Vector3(1, 1, 1);
            newText.GetComponent<Text>().text = chatMessageSent;
            myNumber++;

            chatInputField.text = "";
        } else if (messageSent) //if message has been sent we can clear message
        {
            chatMessageSent = "";
        }

        
        if (chatMessageRecieved != "" && !photonView.isMine)
        {
            GameObject newText = (GameObject)Instantiate(myTextPrefab);
            newText.transform.SetParent(myPanel);
            newText.transform.localPosition = new Vector3(newText.transform.localPosition.x, newText.transform.localPosition.y, 1);
            newText.transform.localScale = new Vector3(1, 1, 1);
            newText.GetComponent<Text>().text = chatMessageRecieved;
            myNumber++;
            chatMessageRecieved = "";
        }
        
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //this is us, need to send new chat message
            stream.SendNext(chatMessageSent);
            messageSent = true;
            if (chatMessageSent != "")
            {
                Debug.Log("ChatMessageSent: " + chatMessageSent);
            }
        }
        else
        {
            //this is some other player on network, needs to recieve this players message
            chatMessageRecieved = (string)stream.ReceiveNext();
            if (chatMessageRecieved != "")
            {
                Debug.Log("ChatMessageRecieved: " + chatMessageRecieved);
            }
        }
        
    }
}
