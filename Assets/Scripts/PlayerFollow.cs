using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {
	
	private Transform PlayerTransform;
	public float CameraOffsetLimitX = 0.3f;
	public float CameraOffsetLimitY = 0.5f;

    public bool gameStarted = false;
    public bool gameFinished = false;
    public bool searchForPlayer = false;
    private bool readyToPlay = false;

    Camera cam;
    Vector3 newPos;
    Vector3 posInicial;
    float tamañoInicial;

    // Use this for initialization
    void Start () {
        posInicial = transform.position;
        cam = GetComponent<Camera>();
        tamañoInicial = cam.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameStarted)
        {
            Follow();
        }
        if (gameFinished)
        {
            RegresaAlInicio();
        }
	}

    void Follow()
    {
        if (searchForPlayer)
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            readyToPlay = (PlayerTransform != null);
            searchForPlayer = !readyToPlay;
        }
        else if (readyToPlay)
        {
            newPos = new Vector3(PlayerTransform.position.x * CameraOffsetLimitX, PlayerTransform.position.y * CameraOffsetLimitY, -1f);

            if (cam.orthographicSize != 3)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, 0.25f);
            }
            transform.position = Vector3.Lerp(transform.position, newPos, 0.25f);
        }
    }

    void RegresaAlInicio()
    {
        transform.position = Vector3.Lerp(transform.position, posInicial, 0.5f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, tamañoInicial, 0.25f);
    }
}