using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        public float health = 100f;         // Enemy's health starts at 100

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DealDamage(float damage)
        {
            health -= damage;               // SET health -= damage

            if(health <= 0)                 // IF health <= 0
            {
                Destroy(gameObject);        // Destroy the enemy
            }
                   
        }
    }
}
