using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public GameObject mainNonPlayerCamera;
    public static NetworkManager control;
    public bool connectedToMaster = false;
    private string displayName = "player";
    private string roomName = "Default Room Name";
    private ExitGames.Client.Photon.Hashtable hash;

    // Use this for initialization
    void Start()
    {
        if (control == null)
        {
            control = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0.0");
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        //PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Random Failed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnPhotonJoinFailed()
    {
        Debug.Log("Failed");
    }

    void OnConnectedToMaster()
    {
        connectedToMaster = true;
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        //SpawnMyPlayer();
        SpawnChatPrefab();

        PhotonPlayer[] otherPlayers = PhotonNetwork.otherPlayers;
        
        hash = new ExitGames.Client.Photon.Hashtable();

        int position = 0;
        if (otherPlayers.Length > 0)
        {
            position = (int)otherPlayers[otherPlayers.Length - 1].CustomProperties["position"];
            position = position + 1;
        }

        hash.Add("position", position);

        PhotonNetwork.player.SetCustomProperties(hash);

        //Debug.Log("position: " + position);

    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.room.PlayerCount == 1)
        {
            //remove room when leaving
            PhotonNetwork.room.MaxPlayers = 0;
            PhotonNetwork.room.IsVisible = false;
            PhotonNetwork.room.IsOpen = false;
        }
        PhotonNetwork.LeaveRoom();
    }

    void SpawnChatPrefab()
    {
        GameObject chatManager = (GameObject)PhotonNetwork.Instantiate("ChatManager", new Vector3(0, 0, -2), Quaternion.identity, 0);
    }

    void SpawnMyPlayer()
    {
        //spawn player and set mainCamera to be disabled so it does not interfer with the player camera, also disables it's audio listener
        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("RedTile", new Vector3(0, 0, -2), Quaternion.identity, 0);
        mainNonPlayerCamera.SetActive(false);

        //keeps other players that are spawned in from having their controlls and camera enabled
        //player object requires move script and camera to be disabled for this to work
        //myPlayer.GetComponent<Camera>().enabled = true;
        //myPlayer.GetComponent<SimpleMove>().enabled = true;
    }

    public void SetRoomSize(int maxRoomSize)
    {
        PhotonNetwork.room.MaxPlayers = maxRoomSize;
    }

    public PhotonPlayer[] GetOtherPlayers()
    {
        return PhotonNetwork.otherPlayers;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public void SetDisplayName(string name)
    {
        displayName = name;
    }

    public string GetRoomName()
    {
        return roomName;
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }
}
