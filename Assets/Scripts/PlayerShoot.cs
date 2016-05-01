using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour 
{
	[SerializeField] private bool isShooting;
	[SerializeField] private bool canShoot = true;
	private ParticleSystem ps;
	// Use this for initialization
	void Start () 
	{
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		isShooting = Input.GetAxis("Fire1") > 0 || Input.GetAxis("Fire2") > 0 || Input.GetAxis("Fire3") > 0;
		if(isShooting && canShoot)
		{
			StartShoot();
		}
		else if (!isShooting && !canShoot)
		{
			StopShoot();
		}
	}
	
	void StartShoot()
	{
		ps.Play();
		canShoot = false;
	}
		
	void StopShoot()
	{
		ps.Stop();
		canShoot = true;
	}
}
