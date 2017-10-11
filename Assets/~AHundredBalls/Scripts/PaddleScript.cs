using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHundredBalls
{
    public class PaddleScript : MonoBehaviour
    {
        private Animator anim;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>(); // Get the animator component
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.DownArrow)) // Check if down arrow is pressed
            {
                anim.SetBool("isOpened", true); // Modify parameter we created earlier
            }

            else // If the button isn't pressed
            {
                anim.SetBool("isOpened", false); // Set parameter to FALSE
            }
        }
    }
}
