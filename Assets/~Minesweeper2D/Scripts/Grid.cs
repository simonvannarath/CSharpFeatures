using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = .155f;

        //public bool isMine = false;

        private Tile[,] tiles;

        // Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; // Position tile
            Tile currentTile = clone.GetComponent<Tile>(); // Get Tile component
            return currentTile; // return it
        }

        void GenerateRand()
        {
            // Create byte array
            const int TOTAL_BYTES = 4;
            byte[] byteArray = new byte[TOTAL_BYTES];

            // Generate Cryprographic class object
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            // Generate a random byte and store it
            crypto.GetBytes(byteArray);

            // Convert the byte array to an int
            int seed = BitConverter.ToInt32(byteArray, 0);

            // Generate a random object with the random seed
            System.Random random = new System.Random(seed);

            // Generate a random float between 1 and 99
            const int MIN = 0;
            const int MAX = 50;

            int randomInt = random.Next(MIN, MAX);
            float randomFloat = randomInt * 0.1f;

        }

        //Spawns tiles in a grid-like pattern
        void GenerateTiles()
        {
            // Create new 2D array of size width x height
            tiles = new Tile[width, height];

            // Loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Store half size for later use
                    Vector2 halfSize = new Vector2(width / 2.25f, height / 2.25f);

                    // Pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);

                    // Apply spacing
                    pos *= spacing;

                    // Spawn the tile
                    Tile tile = SpawnTile(pos);

                    // Attach newly spawned tile to
                    tile.transform.SetParent(transform);

                    // Store its array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;

                    // Store tile in array at those coordinates
                    tiles[x, y] = tile;
                }
            }
        }

        void FixedUpdate()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
                if (hit.collider != null)
                {
                    // Let tile == hit collider's Tile component
                    Tile tile = hit.collider.GetComponent<Tile>();
                    
                    // If tile =! null
                    if(tile != null)
                    {
                        // Let adjacentmines = GetAdjacentMineCountAt
                        int adjacentMines = GetAdjacentMineCountAt(tile);

                        // Call tile.Reveal(adjacentMines)
                        tile.Reveal(adjacentMines);
                    }
                }
            }   
        }

        // Count adjacent mines at element
        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;

            for (int x = -1; x <= 1; x++)
            {
                // Loop through all elements and have each axis go between -1 to 1
                for (int y = -1; y <= 1; y++)
                {
                    // Calculate desired coordinates from ones attained
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;

                    if (desiredX >= 0 && desiredY >= 0 && desiredX < width && desiredY < height)
                    {
                        Tile tile = tiles[desiredX, desiredY];

                        if (tile.isMine)
                        {
                            count++;
                        }
                    }

                  

                    // If desiredX is within range of tiles array length
                    // If the element at index is a mine
                    // Increment count by 1
                }
            }
            

            return count;
        }

        // Use this for initialisation
        void Start()
        {
            GenerateTiles();
        }



    }
}