using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkManager : Photon.PunBehaviour {
	
	[SerializeField] private Transform[] spawnPoints;
    public static string guiMessage;

    GameObject UICanvasMenu;
    GameObject UICanvasPerder;
    PlayerFollow pf;
    string roomName = string.Empty;

    RoomInfo[] avaliableRooms;
    TypedLobby typedLobby;

    public Button unirseSalaBtn;
    public Button crearSalaBtn;
    public Text statusBar;

    public Color[] playerColors;

    AddListItem ali;

    // Use this for initialization
    void Start ()
    {
        pf = FindObjectOfType<Camera>().GetComponent<PlayerFollow>();

        UICanvasPerder = UICanvasMenu = transform.Find("Canvas").transform.Find("Panel Perder").gameObject;
        UICanvasMenu = transform.Find("Canvas").transform.Find("Panel Menu").gameObject;
        ali = UICanvasMenu.transform.Find("Lista de Salas").transform.Find("ElementGrid").GetComponent<AddListItem>();
		spawnPoints = transform.GetComponentsInChildren<Transform>();
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("1.0a");
        }
        typedLobby = new TypedLobby("Main", LobbyType.Default);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(typedLobby);
        foreach(RoomInfo room in avaliableRooms)
        {
            Debug.Log(room);
        }
    }

    public override void OnJoinedLobby()
    {
        crearSalaBtn.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerObject", GenerateRandomSpawnPoint().position, GenerateRandomSpawnPoint().rotation, 0);
        pf.gameStarted = true;
        pf.searchForPlayer = true;
        UICanvasMenu.SetActive(false);
    }
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Join Room Failed");
	}
	
	void OnGUI()
	{
#if UNITY_EDITOR
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label("Player ID: "+ PhotonNetwork.player.ID.ToString());
        GUILayout.Label("Lobby: " + PhotonNetwork.lobby);
        GUILayout.Label("Selected Room: " + roomName);
        GUILayout.Label("Joined Room: " + PhotonNetwork.room);
        GUILayout.Label("Rooms: " + avaliableRooms.Length);
        GUILayout.Label("Ping: " + PhotonNetwork.GetPing());
#endif
    }

    void LateUpdate()
    {
        avaliableRooms = PhotonNetwork.GetRoomList();
        CreateRoomList();

        statusBar.text = PhotonNetwork.connectionStateDetailed.ToString();

        unirseSalaBtn.interactable = (roomName != "" && !PhotonNetwork.connecting);
        crearSalaBtn.interactable = !PhotonNetwork.connecting;

        if(pf.gameFinished)
        {
            UICanvasPerder.SetActive(true);
        }
    }
	
	public Transform GenerateRandomSpawnPoint()
	{
		Transform spawnPoint;
		spawnPoint = spawnPoints[PhotonNetwork.player.ID].transform;
		return spawnPoint; 
	}

    public void CreateNetworkRoom()
    {
        //Manage Room Options
        RoomOptions roomOpts = new RoomOptions();
        roomOpts.maxPlayers = 10;
        //Create the room
        roomName = "TEST" + Random.Range(1,1000);
        PhotonNetwork.CreateRoom(roomName, roomOpts, typedLobby);
    }

    public void SelectRoom(string selectedRoomName)
    {
        roomName = selectedRoomName;
    }

    public void JoinSelectedRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void CreateRoomList()
    {
        avaliableRooms = PhotonNetwork.GetRoomList();
        foreach (RoomInfo roomInfo in avaliableRooms)
        {
            ali.InstantiateListItem(roomInfo.name);
        }
    }

    public void SalirDeJuego()
    {
        Application.Quit();
    }

    public void DejarSala()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}