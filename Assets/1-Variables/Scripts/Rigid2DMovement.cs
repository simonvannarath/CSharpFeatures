using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigid2DMovement : MonoBehaviour 
{
	public float movementSpeed = 180f;
	public float rotationSpeed = 100f;
	public float deceleration = 10f;

	private Rigidbody2D rigid2D;


	// Use this for initialization
	void Start () 
	{
		rigid2D = GetComponent<Rigidbody2D>();
	} // Scope
	
	// Update is called once per frame
	void Update () 
	{
		// Call Movement
		Movement();
		Decelerate();
		Rotation();
	}

	// <return-type> <function-name>
	void Movement()
	{
		float inputV = Input.GetAxis("Vertical");
		// Move by a force "forward" (which is direction right)
		rigid2D.AddForce(transform.right * inputV * movementSpeed);

	}

	void Decelerate()
	{
		// Current velocity + negative current velocity * deceleration
		rigid2D.velocity += -rigid2D.velocity * deceleration * Time.deltaTime;
	}
	void Rotation()
	{
		float inputH = Input.GetAxis("Horizontal");
		// rotate playere around an axis
		transform.rotation *= Quaternion.AngleAxis(-inputH * rotationSpeed * Time.deltaTime, Vector3.forward);
	}
}
