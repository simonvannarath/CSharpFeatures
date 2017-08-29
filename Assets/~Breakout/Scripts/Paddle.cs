using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        public bool isFired = false;
        public float movementSpeed = 20f;   // Speed the paddle moves
        public Ball currentBall;            // Ball that should be attatched to the Paddle as as child
        public Vector2[] directions = new Vector2[] // List of directions for the ball to choose from

        {
            new Vector2 (-0.5f, 0.5f),
            new Vector2 (0.5f, 0.5f)
        };

	    // Use this for initialization
	    void Start ()
        {
            // Grabs currentBall from children of the Paddle
            currentBall = GetComponentInChildren<Ball>();
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            CheckInput();
            Movement();
	    }

        void Fire()
        {
            // Detach as child
            currentBall.transform.SetParent(null);

            // Generate random dir from list of directions
            Vector3 randomDir = directions[Random.Range(0, directions.Length)];

            // Fire off ball in randomDirection
            currentBall.Fire(randomDir);

            // Set isFired to true
            isFired = true;
        }

        void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isFired == false)
                {
                    Fire();
                }
            }
        }

        void Movement()
        {
            // Get input on the horizontal axis
            float inputH = Input.GetAxis("Horizontal");

            // Set force to direction (inputH to decide which direction)
            Vector3 force = transform.right * inputH;

            // Apply movement speed to force
            force *= movementSpeed;

            // Apply deltaTime to force
            force *= Time.deltaTime;

            // Add force to position
            transform.position += force;
        }
    }
}
