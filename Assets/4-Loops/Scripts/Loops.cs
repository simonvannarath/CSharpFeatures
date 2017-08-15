using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopsArrays
{
    public class Loops : MonoBehaviour
    {
        public GameObject[] spawnPrefabs;
        public float frequency = 5;
        public float amplitude = 6;
        public float spawnRadius = 5f;
        public string  message = "Print This";
        public float printTime = 2f;
        public int spawnAmount = 10;

        private float timer = 0;

        // Use this for initialization

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        void Start ()
        {
            /* while (condition)
             * {
             *      // Statement
             * }
             */

            // Infinite loop
            /* int iteration = 0;
             while (iteration < 1000)
             {
                 print(message);
                 iteration++;
             } */

            //SpawnObjects();
            SpawnObjectsWithSine();
        }
	
	    // Update is called once per frame
	    void Update ()
        {
            
            //// Loop through until timer gets to printTime
            //while (timer <= printTime) // STICK AROUND
            //{
            //    //Count up timer in seconds
            //    timer += Time.deltaTime;
            //    print("PUT THE COOKIE DOWN!");
            //    break;
            //}
	    }

       /* void SpawnObjects()
        

        {
            //
             * for (initialisation; condition; iteration)
             * {
             *      // Statement(s)
             * }
             * 
             //

            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawn new GameObject
                GameObject clone = Instantiate(spawnPrefab);

                //Generate random position with circle area (technically shpere)
                Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;

                // Cancel out the Z
                randomPos.z = 0;

                // Set spawned object's position
                clone.transform.position = randomPos;
            }
        } */

      

        void SpawnObjectsWithSine()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawn new GameObject
                int randomIndex = Random.Range(0, spawnPrefabs.Length);

                // Store randomly selected prefab
                GameObject randomPrefab = spawnPrefabs[randomIndex];

                //Instantiate randomly selected prefab
                GameObject clone = Instantiate(randomPrefab);

                // Grab the MeshRenderer
                MeshRenderer rend = clone.GetComponent<MeshRenderer>();

                // Change the colour
                float r = Random.Range(0, 2);
                float g = Random.Range(0, 2);
                float b = Random.Range(0, 2);
                float a = Random.Range(0, 2);

                rend.material.color = new Color(r, g, b, a);

                //Generate random position with circle area (technically shpere)
                // float x = Mathf.Sin(i * frequency) * amplitude;
                // float y = i;
                // float z = 0;

                float x = Mathf.Sin(i * frequency) * amplitude;
                float y = i;
                float z = Mathf.Cos(i * frequency) * amplitude;

                Vector3 randomPos = transform.position + new Vector3(x, y, z);

                // Cancel out the Z
                // randomPos.z = 0;

                // Set spawned object's position
                clone.transform.position = randomPos;
            }
        }
    }

}

