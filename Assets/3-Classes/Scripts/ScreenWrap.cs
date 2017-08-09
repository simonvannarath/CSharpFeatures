using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script depends on the SpriteRenderer Component attached to this object

[RequireComponent(typeof(SpriteRenderer))]

public class ScreenWrap : MonoBehaviour 
{

	// sprite
	private SpriteRenderer spriteRenderer;

	// camera
	private Bounds camBounds;
	private float camWidth;
	private float camHeight;



	// Use this for initialization
	void Awake () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void UpdateCameraBounds()
	{
		// Calculate camera bounds
		Camera cam = Camera.main;
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		camBounds = new Bounds(cam.transform.position, new Vector2(camWidth, camHeight));

	}

	// LateUpdate is called at the end of a frame
	void LateUpdate () 
	{
		UpdateCameraBounds();

		// Store position and size in shorter variable names
		Vector3 pos = transform.position;
		Vector3 size = spriteRenderer.bounds.size;


		// Calculate the sprite's width/2 and height/2
		float halfWidth = size.x / 2;
		float halfHeight = size.y / 2;
		float halfCameraWidth = camWidth / 2;
		float halfCameraHeight = camHeight / 2;

		// Check Left
		if(pos.x + halfWidth < camBounds.min.x)
		{
			pos.x = camBounds.max.x + halfWidth;
		}

		// Check Right
		if(pos.x - halfWidth > camBounds.max.x)
		{
			pos.x = camBounds.min.x - halfWidth;
		}
		// Check Top
		if(pos.y + halfHeight > camBounds.max.y)
		{
			pos.y = camBounds.min.y - halfHeight;
		}

		// Check Bottom
		if(pos.y + halfHeight < camBounds.min.x)
		{
			pos.y = camBounds.max.y + halfHeight;
		}

		// Move the enemy back around aka set position
		transform.position = pos;



	}
}
