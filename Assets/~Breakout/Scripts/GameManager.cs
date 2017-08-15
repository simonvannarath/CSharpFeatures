using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class GameManager : MonoBehaviour
    {

        public int width = 20;
        public int height = 20;
        public Vector2 spacing = new Vector2(25f, 10f);
        public Vector2 offset = new Vector2(-25f, 0f);
        public GameObject[] blockPrefabs;
        [Header("Debug")]
        public bool isDebugging = false;

        private GameObject[,] spawnedBlocks;

        
        // Use this for initialization
        void Start()
        {
            GenerateBlocks();
        }


        GameObject GetRandomBlock()
        {
            int randomIndex = Random.Range(0, blockPrefabs.Length);
            GameObject randomPrefab = blockPrefabs[randomIndex];
            GameObject clone = Instantiate(randomPrefab);
            return clone;
        }

        // Function with arguments
        // <return-type> <function-name> (arguments)
        GameObject GetBlockByIndex(int index)
        {
            // Error handling
            if (index > blockPrefabs.Length || index < 0)
                return null;

            // Create a new block at given index
            GameObject clone = Instantiate(blockPrefabs[index]);
            // .. and return it
            return clone;
        }

        void GenerateBlocks()
        {
            // 
            spawnedBlocks = new GameObject[width, height];

            // Loop through the width
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Create new instance of the block
                    GameObject block = GetRandomBlock();

                    // Set the new position
                    Vector3 pos = new Vector3(x * spacing.x, y * spacing.y, 0);
                    block.transform.position = pos;

                    // Add spawned blocks to array
                    spawnedBlocks[x, y] = block;
                }
            }
        }

        void UpdateBlocks()
        {
            // Loop through entire 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Update possies
                    GameObject currentBlock = spawnedBlocks[x, y];

                    // Create a new possie
                    Vector2 pos = new Vector2(x * spacing.x, y * spacing.y);

                    // Add and offset to pos
                    pos += offset;

                    // Set currentBlock's position to the new pos
                    currentBlock.transform.position = pos;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (isDebugging)
            {
                UpdateBlocks();
            }
        }
    }
}


