using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variables 
{
	public class Movement : MonoBehaviour {
		public float movementSpeed = 20f;
		public float rotationSpeed = 100f;
		public Vector3 rotateDir = Vector3.forward;

		// Use this for initialization
		void Start ()
		{

		}
		// Update is called once per frame
		void Update () 
		{
			// Get horizontal input 
			float inputH = Input.GetAxis("Horizontal");

			// Get vertical input
			float inputV = Input.GetAxis("Vertical");

			// Move the player
			// Drection * Input(sign) * Speed * Delta Time
			Vector3 translation = transform.right * inputV * movementSpeed * Time.deltaTime;
			transform.position += translation;

			// Alternatively : transform.Translate(new Vector3(inputH, inputV) * movementSpeed * Time.deltaTime);
			// Velocity * Delta Time
			// Direction * Input(sign) * Speed * Delta Time
			transform.eulerAngles += rotateDir * -inputH * rotationSpeed * Time.deltaTime;
		}
	}
}
