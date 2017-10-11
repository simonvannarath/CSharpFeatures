using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHundredBalls
{
    public class SpawnerScript : MonoBehaviour
    {
        public GameObject[] prefabs = null;
        public float spawnRadius = 5.0f;
        public float spawnRate = 1.0f;

        private float spawnFactor = 0.0f;
        private Renderer[] renderers;
        private Color[] colors = new Color[6];

        private void Start()
        {
            renderers = GetComponentsInChildren<Renderer>();

            colors[0] = Color.cyan;
            colors[1] = Color.red;
            colors[2] = Color.green;
            colors[3] = new Color(255, 165, 0);
            colors[4] = Color.yellow;
            colors[5] = Color.magenta;

        }
        // Update is called once per frame
        void Update()
        {
            HandleSpawn();
        }

        void HandleSpawn()
        {
            spawnFactor += Time.deltaTime;

            if (spawnFactor > spawnRate) // When the spawn factor time reaches the interval (rate)
            {
                int randomIndex = Random.Range(0, prefabs.Length); // Get a random index into the array
                Spawn(prefabs[randomIndex]); // Spawn a random prefab from the list
                spawnFactor = 0; // Reset spawn factor (timer)
            }

        }

        void Spawn(GameObject _object)
        {
            GameObject newObject = Instantiate(_object);
            Vector3 randomPoint = Random.insideUnitCircle * spawnRadius; // Generate random spawn object

            newObject.transform.position = transform.position + randomPoint; // Set new object's position to random one
            
            if (newObject.gameObject.tag == "Ball")
            {
                newObject.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
            }
        }
    }
}