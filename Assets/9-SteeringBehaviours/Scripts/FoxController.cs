using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FoxController : MonoBehaviour
{
    private Animator anim;
    private Vector3 prevPos;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 currPos = transform.position;
        float movementSpeed = (prevPos - currPos).magnitude;
        anim.SetFloat("moveSpeed", movementSpeed);

        prevPos = currPos;

        
	}
}
