using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Billiards
{
    public class Ball : MonoBehaviour
    {
        public float gravity = -9.81f;

        private Rigidbody rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Ball ball = other.collider.GetComponent<Ball>();
            if(ball != null)
            {
                ball.Activate();
            }
        }

        public void Activate()
        {
            rigid.constraints = RigidbodyConstraints.None;
        }

        public void Deactivate()
        {
            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }

        // FixedUpdate is called
        void FixedUpdate()
        {
            rigid.velocity = rigid.velocity.normalized + Vector3.back * gravity;
        }
    }

}
