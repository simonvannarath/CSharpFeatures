using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float respawnTime = 3f;

    private Vector3 spawnPos;
    private Renderer rend;


    void Awake()
    {
        rend = GetComponent<Renderer>();    
    }
    
    // Use this for initialization
    void Start()
    {
        spawnPos = transform.position; // Recording start position	
	}
	
    public void Spawn()
    {
        StartCoroutine(SpawnDelay());      // Start SpawnDelay coroutine
    }
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnDelay()
    {
        rend.enabled = false;                                   // Disable renderer
        yield return new WaitForSeconds(respawnTime);           // Wait for respawnTime (seconds)
        transform.position = spawnPos;                          // Reset position to spawnPos
        rend.enabled = true;                                    // Enable renderer
    }
}
