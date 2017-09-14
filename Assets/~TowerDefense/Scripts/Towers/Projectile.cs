using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        public float damage = 50f;                              // Damage dealt to whatedver gets hit
        public float speed = 50f;                               // Speed the projectile travels
        public Vector3 direction;                               // Direction the projectile travels
        public Vector3 velocity;

        private Enemy e;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           velocity = direction.normalized * speed;              // LET velocity = direction.normalize * speed
           transform.position += velocity * Time.deltaTime;      // SET projectile's pos += velocity * deltaTime
        }

        void OnTriggerEnter(Collider col)
        {
            e = col.GetComponent<Enemy>();                       // LET e = col's Enemy component
            
            if (e != null)                                       // IF e != null
            {
                e.DealDamage(damage);                            // CALL e.DealDAmage(damage)
                Destroy(gameObject);                             // Destroy gameObject

                if (col.gameObject.name == "Ground")             // IF col's name == "Ground"
                {
                    Destroy(gameObject);                         // Destroy the projectile
                }
            }           
        }
    }
}