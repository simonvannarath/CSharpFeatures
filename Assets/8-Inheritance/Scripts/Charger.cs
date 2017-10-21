using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Charger : Enemy
    {
        [Header("Charger")]
        public float impactForce = 10f;
        public float knockback = 10f;

        public GameObject dashParticles;

        protected override void Attack()
        {
            
        }

        public void Charge()
        {

        }


    }

}

