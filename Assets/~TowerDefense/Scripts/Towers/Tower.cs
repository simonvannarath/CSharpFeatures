using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        public Cannon cannon;                                   // Reference to cannon inside of tower
        public float attackRate = 0.25f;                        // Rate of attack in seconds
        public float attackRadius = 5f;                         // Distance of attack in world units

        private float attackTimer = 0f;                         // Timer to count up to attackRate
        private List<Enemy> enemies = new List<Enemy>();        // List of enemies within radius

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            attackTimer = attackTimer + Time.deltaTime;         // SET attackTimer = attackTime + deltaTime

            if (attackTimer >= attackRate)                      // IF attackTimer >= attackRate
            {
                Attack();                                       // CALL Attack()
                attackTimer = 0;                                // SET attackTimer = 0
            }       
        }

        void OnTriggerEnter(Collider col)
        {
            Enemy e = col.GetComponent<Enemy>();                // LET e = col's Enemy Component

            if (e != null)                                      // IF e != null
            {
                enemies.Add(e);                                 // Add e to enemies list
            }                          
        }

        void OnTriggerExit(Collider col)
        {
            Enemy e = col.GetComponent<Enemy>();                // LET e = col's Enemy Component

            if (e != null)                                      // IF e != null
            {
                enemies.Remove(e);                              // Remove e to enemies list
            }
        }

        Enemy GetClosestEnemy()
        {
            Enemy closest = null;                               // LET closest = null
            float minDistance = float.MaxValue;                 // LET minDistrance = float.MaxValue

            foreach (Enemy enemy in enemies)                    // FOREACH enemy in enemies
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);   // LET distance = the distance between transform's pos and enemy's pos

                if (distance < minDistance)                     // IF distance < minDistance
                {
                    minDistance = distance;                     // SET minDistance = distance
                    closest = enemy;                            // SET closest = enemy
                }
            }                        
            return closest;                                     // RETURN closest
        }

        void Attack()
        {
            Enemy closest = GetClosestEnemy();                  // LET closest to GetClosestEnemy()
            if (closest != null)                                // IF closest != null
            {
                cannon.Fire(closest);                           // CALL cannon.Fire() and pass closest as argument
            }
        }
    }

}