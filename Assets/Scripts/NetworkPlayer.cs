using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public Text labelVida;
    [SerializeField]
    int vida = 100;

    Vector2 realPosition;
    Quaternion realRotation;
    Vector3 escalaReal;
    Color colorReal;

    public GameObject prefabParticulasMuerte;
    public bool NetworkShooting;

    private PlayerFollow pf;
    NetworkManager nm;
    private Color miColor;

    // Use this for initialization
    void Start ()
    {
        nm = FindObjectOfType<NetworkManager>();

        name = name + PhotonNetwork.player.ID.ToString();

        pf = GameObject.FindObjectOfType<Camera>().GetComponent<PlayerFollow>();
        GetComponent<SpriteRenderer>().color = nm.playerColors[Random.Range(0, nm.playerColors.Length)];
        miColor = GetComponent<SpriteRenderer>().color;

        if (photonView.isMine)
        {
            GetComponent<PlayerMotor>().enabled = true;
        }
        else
        {
            gameObject.tag = "NetworkPlayer";
            gameObject.layer = 9;
        }
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector2.Lerp(transform.position, realPosition, 0.05f);
            transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, realRotation.eulerAngles.z, 0.05f));
            transform.localScale = Vector3.Lerp(transform.localScale, escalaReal, 0.1f);
            miColor = colorReal;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext((Vector2)transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
            stream.SendNext(vida);
            stream.SendNext(miColor.a);
            stream.SendNext(miColor.r);
            stream.SendNext(miColor.g);
            stream.SendNext(miColor.b);
        }
        else
        {
            realPosition = (Vector2)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            escalaReal = (Vector3)stream.ReceiveNext();
            vida = (int)stream.ReceiveNext();
            colorReal.a = (float)stream.ReceiveNext();
            colorReal.r = (float)stream.ReceiveNext();
            colorReal.g = (float)stream.ReceiveNext();
            colorReal.b = (float)stream.ReceiveNext();
        }
    }
    
    [PunRPC]
    public void TakeDamage(int IdJugador)
    {
        vida -= 30;
        if (vida < 1)
        {
            if (photonView.isMine)
            {
                BuscaJugador(IdJugador);
                Debug.Log("Player " + IdJugador + " killed player " + PhotonNetwork.player.ID);
                PhotonNetwork.Instantiate("ExplosionGrande", transform.position, transform.rotation, 0);
                pf.gameStarted = false;
                pf.gameFinished = true;
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    public void JugadorCrece()
    {
        transform.localScale += (Vector3.one * 0.5f);
    }

    public void BuscaJugador(int ID)
    {
        GameObject[] jugadores = GameObject.FindGameObjectsWithTag("NetworkPlayer");

        foreach (GameObject jugador in jugadores)
        {
            if(jugador.GetComponent<PhotonView>().owner.ID == ID)
            {
                jugador.GetPhotonView().RPC("JugadorCrece", PhotonTargets.All);
            }
        }
    }
}