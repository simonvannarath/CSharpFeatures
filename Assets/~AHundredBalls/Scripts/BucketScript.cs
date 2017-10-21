using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHundredBalls
{
    public class BucketScript : MonoBehaviour
    {
        const int AMOUNT = 1;
        public float movementSpeed = 10.0f;
        
        private GameManager gameManager;
        private Rigidbody2D rigid2D;
        private Renderer[] renderers;

        // Use this for initialization
        void Start()
        {
            rigid2D = GetComponent<Rigidbody2D>();
            renderers = GetComponentsInChildren<Renderer>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            HandlePosition();
            HandleBoundary();
        }

        void HandlePosition() // Handles bucket position
        {
            rigid2D.velocity = Vector3.left * movementSpeed;
        }

        void HandleBoundary() // Handles the screen bouhndaries for game object
        {
            Vector3 transformPos = transform.position;
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transformPos); // Get the viewport position of where the bucket is

            if (IsVisible() == false && viewportPos.x < 0) // Is the GameObject visible from the camera an on the left sife of it?
            {
                Destroy(gameObject); // Then destroy the GameObject
            }
        }

        bool IsVisible() // Checks whether or not any renderer attached to this GameObject and its cheldren are visible
        {
            foreach (var renderer in renderers)
            {
                if (renderer.isVisible)
                {
                    return true;
                }
            }

            return false;
        }

        // Detect Trigger collisions
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ball")
            {
                gameManager.IncrementScore(AMOUNT);
                other.GetComponent<TrailRenderer>().enabled = false;
            }
        }
    }
}