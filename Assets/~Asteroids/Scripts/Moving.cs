using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Moving : MonoBehaviour
    {
        public float accelerate = 50f;
        public float rotationSpeed = 100f;

        void Awake()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        }
        
        // Use this for initialization
        void Start()
        {

        }

        void Accelerate()
        {
            float inputV = Input.GetAxis("Vertical");
            GetComponent<Rigidbody>().AddForce(transform.up * inputV * accelerate);
        }

        void Rotate()
        {
            float inputH = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.back, rotationSpeed * inputH * Time.deltaTime);
        }

        // Update is called once per frame
        void Update()
        {
            Accelerate();
            Rotate();
        }
    }

}
