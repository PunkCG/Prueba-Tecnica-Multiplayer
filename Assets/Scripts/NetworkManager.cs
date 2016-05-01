using UnityEngine;

public class NetworkManager : Photon.PunBehaviour {
	
	[SerializeField] private Transform[] spawnPoints;
    public static string guiMessage;

	// Use this for initialization
	void Start () {
		spawnPoints = transform.GetComponentsInChildren<Transform>();
		PhotonNetwork.ConnectUsingSettings("1.0a");
	}

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void  OnJoinedLobby(){
		Debug.Log("Joined Lobby");
	}

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PlayerFollow.gameStarted = true;
        PhotonNetwork.Instantiate("PlayerObject", GenerateRandomSpawnPoint(), Quaternion.identity, 0);
        PlayerFollow.searchForPlayer = true;
    }
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Join Room Failed, Creating Room");
		PhotonNetwork.CreateRoom("TEST");
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label("Player ID: "+PhotonNetwork.player.ID.ToString());
        GUILayout.Label("Message: " + guiMessage);
    }
	
	public Vector3 GenerateRandomSpawnPoint()
	{
		Vector3 spawnPoint;
		spawnPoint = spawnPoints[PhotonNetwork.player.ID].transform.position;
		return spawnPoint; 
	}
}
