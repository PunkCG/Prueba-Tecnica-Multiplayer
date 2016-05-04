using UnityEngine;
using System.Collections;

public class Balas : Photon.MonoBehaviour {

    Rigidbody2D rb2d;
    public float velocidad;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        transform.Translate(Vector3.up*(velocidad/5));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.SetActive(false);
    }

    void OnPhotonSerializedView()
    {

    }
}
