using UnityEngine;
using System.Collections;

public class Balas : Photon.MonoBehaviour
{
    [Range(1.0f,10.0f)]
    public float velocidad;

    private Vector2 realPosition;
    public GameObject prefabExplosion;

    // Use this for initialization
    void Start ()
    {
        if (!photonView.isMine)
        {
            tag = "NetworkBullet";
            GetComponent<Collider2D>().enabled = false;
        }
	}

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * (velocidad / 25));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (tag == "PlayerBullet" && col.tag == "Player")
        {
            //Las balas chocan contra el mismo jugador que las disparó
        }
        else
        {
            if (col.gameObject.GetPhotonView())
            {
                col.gameObject.GetPhotonView().RPC("TakeDamage", PhotonTargets.All, photonView.ownerId);
            }
            Destruir(col.gameObject);
        }
    }
    
    public void Destruir(GameObject goCol)
    {
        if (photonView.isMine && goCol.name != gameObject.name)
        {
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.Instantiate("Explosion", transform.position, transform.rotation,0);
        }
    }

    [PunRPC]
    public void CallbackMatar()
    {
        
    }
}
