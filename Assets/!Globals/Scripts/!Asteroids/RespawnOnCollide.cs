using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Respawn))]

public class RespawnOnCollide : MonoBehaviour
{
    public string colliderTag;

    private Respawn res;

    void Awake()
    {
        res = GetComponent<Respawn>(); // Ger tthe attached respawn component   
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == colliderTag)
        {
            res.Spawn();
        }
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
