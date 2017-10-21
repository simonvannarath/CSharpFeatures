using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Sploady : Enemy
    {
        [Header("Sploady")]
        public float explosionRadius = 10f;
        public float impactForce = 10f;
        public float explosionRate = 1f;
        public GameObject explosionParticles;

        private float explosionTimer = 0f;

        protected override void Update()
        {
            base.Update();
            // Start Explosion Timer
            explosionTimer += Time.deltaTime;
        }

        protected override void Attack()
        {
            
            // IF explosionTimer reaches rate
            if (explosionTimer >= explosionRate)
            {
                // Call Splode()
                Splode();
            }
        }

        protected override void OnAttackEnd()
        {
            // Reset Explosion Timer
            explosionTimer = 0f;
        }

        public void Splode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);    // Perform overlap sphere Physics.OverlapSphere()
            foreach (Collider hit in hits)          // FOREACH hit in hits
            {
                Health h = hit.GetComponent<Health>(); // IF has health
                if (h != null)
                {
                    h.TakeDamage(damage);               // decrese health
                    rigid.AddExplosionForce(impactForce, transform.position, explosionRadius); // add force to player's rigid
                }
            }
        }
    }
}
