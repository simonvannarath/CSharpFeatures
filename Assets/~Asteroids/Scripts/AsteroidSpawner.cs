using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        public GameObject[] asteroidPrefabs;
        public float spawnRate = 1f;
        public float spawnRadius = 5f;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        // Spawns a new asteroid randomly
        void Spawn()
        {
            Vector3 rand = Random.insideUnitSphere * spawnRadius;       // Generate randomised position
            rand.z = 0f;                                                // Cancel z axis
            Vector3 position = transform.position + rand;               // Offset generated position by transform's position
            int randIndex = Random.Range(0, asteroidPrefabs.Length);    // Generate random index into prefab array
            GameObject randAsteroid = asteroidPrefabs[randIndex];       // Get random asteroid GameObject
            GameObject clone = Instantiate(randAsteroid);               // Clone random asteroid
            clone.transform.position = position;                        // Get position of clone
        }
        
        // Use this for initialization
        void Start()
        {
            InvokeRepeating("Spawn", 0f, spawnRate);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}