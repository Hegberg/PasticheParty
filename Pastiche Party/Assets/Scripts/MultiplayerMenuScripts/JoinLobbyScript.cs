using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinLobbyScript : MonoBehaviour {

    public InputField displayNameField;

    // Use this for initialization
    void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        //InputField name = displayNameField.GetComponent<InputField>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        //NetworkManager.control.Connect();
        if (NetworkManager.control.connectedToMaster)
        {
            if (displayNameField.text != "")
            {
                NetworkManager.control.SetDisplayName(displayNameField.text);
            }
            NetworkManager.control.JoinRandomRoom();
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
