using UnityEngine;
using System.Collections;

public class Balas : Photon.MonoBehaviour {

    Rigidbody2D rb2d;
    public float velocidad;

    Vector2 realPosition;
    Quaternion realRotation;

    public bool IsNetworkBullet;

    // Use this for initialization
    void Start () {
        IsNetworkBullet = !photonView.isMine;
        rb2d = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if (IsNetworkBullet)
        {
            transform.position = Vector2.Lerp(transform.position, realPosition, 0.1f);
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, realRotation.eulerAngles.z, 0.1f));
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up*(velocidad/5));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        PhotonNetwork.Destroy(gameObject);
    }

    void OnPhotonSerializedView(PhotonStream stream)
    {
        if (stream.isWriting)
        {
            stream.SendNext((Vector2)transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realPosition = (Vector2)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
