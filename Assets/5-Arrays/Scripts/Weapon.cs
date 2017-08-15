using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrays
{
    public class Weapon : MonoBehaviour
    {
        public int damage = 10;
        public int maxBullets = 30;
        public float fireInterval = 0.2f;
        public GameObject bulletPrefab;
        public Transform spawnPoint;

        private Transform target;
        private bool isFired = false;
        private int currentBullets = 0;
        private Bullet[] spawnedBullets; // Null by default

        // Use this for initialization
        void Start()
        {
            spawnedBullets = new Bullet[maxBullets];
        }

        // Update is called once per frame
        void Update()
        {
            // IF !isFired && currentBullets < maxBullets
            if(!isFired && currentBullets < maxBullets)
            {
                // Fire! Feuer frei!
                StartCoroutine(Fire());
            }
        }

        IEnumerator Fire()
        {
            // Run whatever is at top of this function
            isFired = true;

            // Spawn a bullet
            SpawnBullet();

            // Wait for fire interval to finish
            yield return new WaitForSeconds(fireInterval); 

            // Run whatever is here after fireInterval
            isFired = false;

        }

        void SpawnBullet()
        {
            // Instantiate a bullet clone
            GameObject clone = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

            // Create direction variable for bullet and rotation
            Vector2 direction = target.position - transform.position;
            direction.Normalize();
            // Rotate the barrel to direction
            transform.rotation = Quaternion.LookRotation(direction);

            // Grab the bullet script from clone
            Bullet bullet = clone.GetComponent<Bullet>();

            // Send bullet to target (by setting direction)
            bullet.direction = direction;

            // Store bullet in array using currentBullets as an index
            spawnedBullets[currentBullets] = bullet;

            // increment currentBullets
            currentBullets++;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}

