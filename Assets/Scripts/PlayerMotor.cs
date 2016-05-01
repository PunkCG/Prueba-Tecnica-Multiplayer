using UnityEngine;
using System.Collections;

public class PlayerMotor : Photon.MonoBehaviour {

	private Rigidbody2D rb2d;
	public float SpeedMultiplier;
    public float RotationSpeed;
    public float DeadZone;
    public bool isMoving;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update(){
		if (Input.GetAxis("Horizontal") < -DeadZone || Input.GetAxis("Horizontal") > DeadZone || Input.GetAxis("Vertical") < -DeadZone || Input.GetAxis("Vertical") > DeadZone)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if(isMoving)
        {
            //Movimiento de jugador
            //
			transform.up = Vector2.Lerp(transform.up, movement, RotationSpeed * Time.deltaTime);
            rb2d.AddForce(transform.up*(SpeedMultiplier*Time.deltaTime));
        }
	}
}
