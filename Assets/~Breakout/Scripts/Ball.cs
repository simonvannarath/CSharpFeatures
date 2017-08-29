using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        public float speed = 25f; // Speed the ball travels
        public int score;

        private Vector3 velocity; // Velocity of the ball (direction x speed)
        private GameManager gameManager;
        // private GameObject[] block;
        private int blockCount;


        const int BLOCKA = 1;
        const int BLOCKB = 2;
        const int decrement = 1;



        // Use this for initialization
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            score = 0;
        }

        // Update is called once per frame
        private void Update()
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

            if (other.gameObject.tag == "Block") // Check if that "other" collision corresponds to a gameObject with a tag called "BlockA"
            {
                // Destroy the object
                Destroy(other.gameObject);

                // Add to score - send the value of BLOCKA to GameManager's AddPoints method (1 point)
                gameManager.AddPoints(BLOCKA);
                gameManager.CountDown(decrement);
            }
            if (other.gameObject.tag == "BlockB") // Check collision corresponds to a gameObject with a "BlockB" tag
            {
                Destroy(other.gameObject);

                // 2 points
                gameManager.AddPoints(BLOCKB);
                gameManager.CountDown(decrement);
            }

            /*
            if (other.gameObject.tag == "Lose")
            {
                gameManager.ResetGame();
            }
            */
            // Calculate the reflection point of the ball using velocity & contact normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);

            // Calculate new velocity from reflection multiplied by the same speed (velocity.magnitude)
            velocity = reflect.normalized * velocity.magnitude;


        }
        // Detect Trigger collisions
        void OnTriggerEnter2D(Collider2D wall)
        {
            if (wall.gameObject.tag == "Lose")
            {
                gameManager.ResetGame();
            }
        }
    }
}



