using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Cannon : MonoBehaviour
    {
        public Transform barrel;                                    // Reference to barrel where bullet will be shot from
        public GameObject projectilePrefab;                         // Prefab of projectile to instantiate when firing

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Fire(Enemy targetEnemy)
        {
            Vector3 targetPos = targetEnemy.transform.position;                         // LET targetPos = targetEnem'y position
            Vector3 barrelPos = barrel.transform.position;                              // LET barrelPos = barrel's position
            Quaternion barrelRot = barrel.transform.rotation;                           // LET barrelRot = barrel's rotation (bearing)
            Vector3 fireDirection = targetPos - barrelPos;                              // LET fireDirection = targetPos - barrelPos
            transform.rotation = Quaternion.LookRotation(fireDirection, Vector3.up);    // SET cannon's rotation = Quaternion.LookRotation(fireDirection, Vector3.up)
            GameObject clone = Instantiate(projectilePrefab, barrelPos, barrelRot);     // LET clone = Instantiate(projectilePrefab, barrelPos, BarrelRot)
            Projectile p = clone.GetComponent<Projectile>();                            // LET p clone's Projectile component
            p.direction = fireDirection;                                                // SET p.direction = fireDirection
        }
    }
}