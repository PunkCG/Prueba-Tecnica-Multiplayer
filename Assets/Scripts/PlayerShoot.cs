using UnityEngine;
using System.Collections;

public class PlayerShoot : Photon.MonoBehaviour 
{
	[SerializeField] private bool estaDisparando;
    private bool flagDisparo = true;
    private Transform puntoInstBullet;
    public GameObject bulletPrefab;

    private NetworkPlayer np;

	// Use this for initialization
	void Start () 
	{
        np = GetComponent<NetworkPlayer>();
        puntoInstBullet = transform.Find("CannonPoint").transform;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (!photonView.isMine)
        {
            estaDisparando = np.NetworkShooting;
        }
        else
        {
            estaDisparando = Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3");
        }

		if(estaDisparando && photonView.isMine)
		{
            if (flagDisparo)
            {
                PhotonNetwork.Instantiate("Bullet", puntoInstBullet.position, puntoInstBullet.rotation, 0);
                flagDisparo = false;
            }
		}
		else if (!estaDisparando && !flagDisparo)
		{
            flagDisparo = true;
		}
	}
}
