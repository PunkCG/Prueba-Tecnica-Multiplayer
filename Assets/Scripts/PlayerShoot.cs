using UnityEngine;
using System.Collections;

public class PlayerShoot : Photon.MonoBehaviour 
{
	[SerializeField] private bool isShooting;
    private bool flagDisparo = true;
    [SerializeField] private int ID;
    public ParticleSystem ps;

    NetworkPlayer np;

	// Use this for initialization
	void Start () 
	{
        np = GetComponent<NetworkPlayer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (np.IsNetworkPlayer)
        {
            isShooting = np.NetworkShooting;
        }
        else
        {
            isShooting = Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3");
        }

		if(isShooting && !np.IsNetworkPlayer)
		{
            if (flagDisparo)
            {
                ps.Play();
                Debug.Log("Disparo local");
                flagDisparo = false;
            }
		}
        else if(np.IsNetworkPlayer)
        {            
            if (np.NetworkShooting)
            {
                if (flagDisparo)
                {
                    ps.Play();
                    Debug.Log("Disparo remoto");
                    flagDisparo = false;
                }
            }
            else if(!flagDisparo)
            {
                Debug.Log("No dispara remoto");
                ps.Stop();
                flagDisparo = true;
            }
        }
		else if (!isShooting && !flagDisparo)
		{
            Debug.Log("No dispara local");
            ps.Stop();
            flagDisparo = true;
		}
	}
}
