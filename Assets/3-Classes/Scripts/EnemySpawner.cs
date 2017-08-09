using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
	public class EnemySpawner : MonoBehaviour 
	{
		public GameObject enemyPrefab;
		public float spawnRate = 1f;
		public float spawnRadius =1f;
		public float force = 300f;

		void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, spawnRadius);
		}

	// Use this for initialization
		void Start () 
		{
			// InvokeRepeating(functionName, time, repeatRate);
			// functionName = name of the function you want to call!
			// time 		= delay for when the function gets called the first time.
			// repeatRate	= how often the function gets called

			InvokeRepeating("Spawn", 0, spawnRadius);

		}
	
	// Update is called once per frame
		void Spawn () 
		{
			// Instantiate a new gameObject
			GameObject enemy = Instantiate(enemyPrefab);

			// Position it to a random location within the spawnRadius
			enemy.transform.position = Random.insideUnitCircle * spawnRadius;

			// Apply force to a rigidbody randomly
			Rigidbody2D rigid2D = enemy.GetComponent<Rigidbody2D>();
			rigid2D.AddForce(Random.insideUnitCircle * force);


		}
	}


}


