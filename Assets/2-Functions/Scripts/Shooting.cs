using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Functions
{
	public class Shooting : MonoBehaviour 
	{

		// Stores the object we want to instantiate
		public GameObject projectilePrefab;

		// Recoil amount from a shot
		public float recoil = 30;

		// Speed at which projectile weill be flung
		public float projectileSpeed = 10f;
		
		// ROF
		public float shootRate = 0.1f;
		
		// Timer to count to shootRate
		private float shootTimer = 0f;

		// Container for Rigidbody2D
		private Rigidbody2D rigid;

		void Start()
		{
			// Get component from gameObject this script is attached to
			rigid = GetComponent<Rigidbody2D>();
		}
	
		// Update is called once per frame
		void Update () 
		{
			// Increase timer
			shootTimer += Time.deltaTime;

			// If spacebar is pressed AND shootTimer >= shootRate
			if(Input.GetKey("space") && shootTimer >= shootRate)
			{
				// CALL Shoot()
				Shoot();
			
				// RESET shootTimer = 0
				shootTimer = 0f;
			}
				
		}

		void Shoot()
			{
			// Instantiate gameObject here
			GameObject projectile = Instantiate(projectilePrefab);
			
			// Position of projectile to Player's position
			projectile.transform.position = transform.position;

			// Get projectile's RigidBody
			Rigidbody2D projectileRigid = projectile.GetComponent<Rigidbody2D>();
			projectileRigid.AddForce(transform.right * projectileSpeed);

			// Apply a recoil
			rigid.AddForce(-transform.right * recoil, ForceMode2D.Impulse);
			}

	}

}
