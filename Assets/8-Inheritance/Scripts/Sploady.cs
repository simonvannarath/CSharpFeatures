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
        public GameObject explosionParticles;

        public override void Attack()
        {

        }

        public void Explode()
        {

        }
    }
}
