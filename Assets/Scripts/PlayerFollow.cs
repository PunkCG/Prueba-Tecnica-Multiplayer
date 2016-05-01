using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {
	
	public Transform PlayerTransform;
	public float CameraOffsetLimitX = 0.3f;
	public float CameraOffsetLimitY = 0.5f;

    public static bool gameStarted = false;
    public static bool searchForPlayer = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (gameStarted)
        {
            if (searchForPlayer)
            {
                PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                GetComponent<Camera>().orthographicSize = 3;
                searchForPlayer = false;
            }
            transform.position = new Vector3(PlayerTransform.position.x * CameraOffsetLimitX, PlayerTransform.position.y * CameraOffsetLimitY, -1f);
        }
	}
}
