using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Breakout
{
    public class GameManager : MonoBehaviour
    {

        public int width = 15;
        public int height = 15;
        public int blockCount;
        public int score;
        public Vector2 spacing = new Vector2(4f, 1.1f);
        public Vector2 offset = new Vector2(-40f, 10f);
        public GameObject[] blockPrefabs;
        public float scrW, scrH;

        private float ballPos;

        [Header("Debug")]
        public bool isDebugging = false;
        private GameObject[,] spawnedBlocks;

        
        // Use this for initialization
        void Start()
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;
            score = 0;
            blockCount = width * height;
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

        void CheckBlockCount()
        {
            if (blockCount <= 0)
            {
                Win();
            }
        }

        public void AddPoints(int amount) // Score increases per block type amount
        {
            score = score + amount;
        }

        public void CountDown(int amount)
        {
            blockCount = blockCount - amount;
        }
        
        public void Win()
        {
            SceneManager.LoadScene("Win");    
        }
        
        // Reset game if ball leaves area
        
        public void ResetGame()
        {
            SceneManager.LoadScene("Breakout");
        }
        
        /*
        ** OR **
        if collide with bottom collider
        restart scene

        */


        // GUI Section
        private void OnGUI()
        {
            GUI.Box(new Rect(1 * scrW, 0.25f * scrH, 0.5f * scrW, 0.5f * scrH), score.ToString()); // score on top
        }


        

        // Update is called once per frame
        void Update()
        {
            CheckBlockCount();

            if (isDebugging)
            {
                UpdateBlocks();
                // blockCount = width * height;
                // Debug.Log("Block count is: " + blockCount.ToString());
            }
        }
    }
}


