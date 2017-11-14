using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    public class UserInput : MonoBehaviour
    {
        private PlayerController pController;

        // Use this for initialization
        void Start()
        {
            pController = GetComponent<PlayerController>(); 
        }

        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            pController.Move(inputH, inputV);

            if(Input.GetKeyDown(KeyCode.Space) &&
               pController.isGrounded)
            {
                pController.Jump();
            }
        }
    }
}