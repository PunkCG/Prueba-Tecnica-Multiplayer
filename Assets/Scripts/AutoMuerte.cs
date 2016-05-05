using UnityEngine;
using System.Collections;

public class AutoMuerte : Photon.MonoBehaviour {

    private float tiempoMuerte;
        
	// Use this for initialization
	void Start () {
        tiempoMuerte = GetComponent<ParticleSystem>().startLifetime;
        StartCoroutine("MatarParticula");
    }

    public IEnumerator MatarParticula()
    {
        yield return new WaitForSeconds(tiempoMuerte);
        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
