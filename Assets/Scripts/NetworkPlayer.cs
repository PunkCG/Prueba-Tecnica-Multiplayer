using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {
	
	public bool IsNetworkPlayer;

    Vector2 realPosition;
    Quaternion realRotation;

    private ParticleSystem MyPS;
    public bool NetworkShooting;

    Rigidbody2D rb2d;

    // Use this for initialization
    void Start ()
    {
        IsNetworkPlayer = !photonView.isMine;
        MyPS = transform.Find("CannonPoint").GetComponent<ParticleSystem>();

        rb2d = GetComponent<Rigidbody2D>();

        if (!IsNetworkPlayer)
        {
            GetComponent<PlayerMotor>().enabled = true;
        }
        else
        {
            gameObject.tag = "NetworkPlayer";
            gameObject.layer = 9;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    void Update()
    {
        if (IsNetworkPlayer)
        {
            transform.position = Vector2.Lerp(transform.position, realPosition, 0.1f);
            transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, realRotation.eulerAngles.z, 0.1f));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext((Vector2)transform.position);
            stream.SendNext(transform.rotation);
            if (MyPS != null)
            {
                stream.SendNext(MyPS.isPlaying);
            }
        }
        else
        {
            realPosition = (Vector2)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            try
            {
                NetworkShooting = (bool)stream.ReceiveNext();
            }
            catch (UnityException e)
            {
                Debug.Log(e);
            }
        }
    }

    public void OnColiisionEnter2D(Collision2D col)
    {
        Debug.Log("Coll");
        rb2d.AddForceAtPosition(Vector2.one * 10, col.transform.position - transform.position);
    }
}