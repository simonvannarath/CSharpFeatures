using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletSpeed = 20f;
        public float shootRate = 0.2f;

        private float shootTimer = 0f;

        // Shoots a bullet
        void Shoot()
        {
            GameObject clone = Instantiate(bulletPrefab, transform.position, transform.rotation); // Create a new bullet
            Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>(); // Grab Rigidbody2D from bullet clone
            rigid.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);        // add Force tusing bullet speed
        }

        // Update is called once per frame
        void Update()
        {
            
            shootTimer += Time.deltaTime;           // Count up shootTimer with deltaTime
            // if Shoot timer > shoot rate
            if (shootTimer > shootRate)             
            {
                // if space is pressed
                if (Input.GetKey(KeyCode.Space))
                {
                    Shoot();                        // Shoot bullet
                    shootTimer = 0;                 // reset shoot timer
                }
            }    
            
                       

        }
    }
}
