using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL; // Git Gud Lol

namespace AI
{
    public class Flee : SteeringBehaviour
    {
        public Transform target;
        public float stoppingDistance = 1f;

        public override Vector3 GetForce()
        {
            Vector3 force = Vector3.zero;                                   // LET force = Vector.zero
            if (target == null)                                             // IF target == null
            {
                return force;                                               // return force
            }

            Vector3 desiredForce = transform.position - target.position;    // LET desiredForce = transform position - target position

            if (desiredForce.magnitude > stoppingDistance)                  // IF desiredForce.magnitude > stoppingDistance
            {
                desiredForce = desiredForce.normalized * weight;            // SET desiredForce = desiredForce.normalized * weighting
                force = desiredForce - owner.velocity;                      // SET force = desiredForce - owner.velocity
            }

            #region GizmosGL
            GizmosGL.color = Color.yellow;
            GizmosGL.AddLine(transform.position, transform.position + force, 0.1f, 0.1f);
            GizmosGL.color = Color.green;
            GizmosGL.AddLine(transform.position, transform.position + desiredForce);


            #endregion

            return force;                                                   // Return the force... luke!
        }
    }
}
