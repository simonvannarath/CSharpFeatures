using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ball")
        {
            // destroy the object
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
