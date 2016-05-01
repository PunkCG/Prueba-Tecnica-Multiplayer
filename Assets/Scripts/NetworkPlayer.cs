using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {
	
	public bool IsNetworkPlayer;

    Vector2 realPosition;
    Quaternion realRotation;

    // Use this for initialization
    void Start ()
    {
        IsNetworkPlayer = !photonView.isMine;

        if (!IsNetworkPlayer)
        {
            GetComponent<PlayerMotor>().enabled = true;
            GetComponentInChildren<PlayerShoot>().enabled = true;
        }
        else
        {
            gameObject.tag = "NetworkPlayer";
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
        Debug.Log("Is Serializing");
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
