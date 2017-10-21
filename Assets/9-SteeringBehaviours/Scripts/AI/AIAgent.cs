using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIAgent : MonoBehaviour
    {
        public float maxSpeed = 10f;
        public float maxDistance = 5f;

        [HideInInspector] public Vector3 velocity;
                                              // total force calculated from behaviours                                                         // direction of travel and speed
        public bool freezeRotation = false;                                                 // do we freeze rotation?

        private Vector3 force;
        private NavMeshAgent nav;
        private List<SteeringBehaviour> behaviours;                                         // List of SteeringBehaviours (i.e. Seek, Flee, Wander, etc.)


        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
        }

        // Use this for initialization
        void Start()
        {
        
        }

        void ComputeForces()
        {
            force = Vector3.zero;                                                           // SET force = Vector3.zero

            for (int i = 0; i < behaviours.Count; i++)                                      // FOR i := 0 < behaviours.Count
            {
                SteeringBehaviour behaviour = behaviours[i];                                // LET behaviour = behaviours[i]
                if (behaviour.isActiveAndEnabled == false)                                  // IF behaviour.isActiveAndEnabled == false
                {
                    continue;                                                               // continue
                }

                force += behaviour.GetForce();                           // SET force = force + behaviour.GetForce() * behaviour.weighting
                
                if (force.magnitude > maxSpeed)                                              // IF force > maxVelocity
                {
                    force = force.normalized * maxSpeed;                                     // SET force = force.normalized * maxVelocity
                    break;                                                                  // break
                }
            }
        }

        void ApplyVelocity()
        {
            velocity += force * Time.deltaTime;                                             // SET velocity = velocity + force * deltaTime

            if (velocity.magnitude > maxSpeed)                                              // IF velocity.magnitude > maxVelocity
            {
                velocity = velocity.normalized * maxSpeed;                                  // SET velocity = velocity.normalized * maxVelocity
            }

            if (velocity.magnitude > 0)                                                     // IF velocity.magnitude > 0
            {
                transform.position += velocity * Time.deltaTime;                            // SET transform.position = transform.position + velocity * deltaTime
                transform.rotation = Quaternion.LookRotation(velocity);                     // SET transform.rotation = Quaternion LookRotation (velocity)
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ComputeForces();
            ApplyVelocity();
        }
    }
}