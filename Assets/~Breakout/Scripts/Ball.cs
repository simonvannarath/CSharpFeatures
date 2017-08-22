using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        public float speed = 10f; // Speed the ball travels

        private Vector3 velocity; // Velocity of the ball (direction x speed)

	    // Use this for initialization
	    void Start () {
		}
	
	    // Update is called once per frame
	    private void Update ()
        {
            // Moves ball using velocity & deltaTime
            transform.position += velocity * Time.deltaTime;
	    }

        // Fires off ball in a given direction
        public void Fire(Vector3 direction)
        {
            // Calculate velocity
            velocity = direction * speed;
        }
        
        // Detect collisions
        void OnCollisionEnter2D(Collision2D other)
        {
            // Grab contact point of collision
            ContactPoint2D contact = other.contacts[0];

            // Calculate the reflection point of the ball using velocity & contact normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);

            // Calculate new velocity from reflection multiplied by the same speed (velocity.magnitude)
            velocity = reflect.normalized * velocity.magnitude;

        }

    }
}


