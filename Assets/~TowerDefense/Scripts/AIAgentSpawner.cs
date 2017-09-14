using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class AIAgentSpawner : MonoBehaviour
    {
        public GameObject aiAgentPrefab;        // Prefab of the AI Agent
        public Transform target;                // Target that each AI Agent should travel to
        public float spawnRate = 1f;            // Rate of spawn
        public float spawnRadius = 1f;          // Radius of spawn

        // Use this for initialization
        void Start()
        {
            // InvokeRepeating(functionNAme, time, repeatRate)
            // functionName = name of the function you want to call in the class
            // time         = delay for when the function gets called the first time
            // repeatRate   = how often the function gets called
            InvokeRepeating("Spawn", 0, spawnRate);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius); // Draw a sphere to indicate the spawn radius
        }

        void Spawn()
        {
            Vector3 rand = Random.insideUnitSphere;                                 // LET rand = Random Inside Unit Sphere
            rand.y = 0;                                                             // set rand.y 0
            Vector3 pos = transform.position + rand * spawnRadius;     // SET clone's pos to transform's pos  + rand * spawnRadius
            GameObject clone = Instantiate(aiAgentPrefab, pos, Quaternion.identity);                          // LET clone = new instance of aiAgentPrefab
            AIAgent aiAgent = clone.GetComponent<AIAgent>();                 // SET aiAgent = clone's AI component
            aiAgent.target = target;                                                // SET aiAgent.target = target
        }
    }
}
