using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public enum MineState
        {
            LOSS = 0, WIN = 1
        }

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

                    // If desiredX is within range of tiles array length
                        // If the element at index is a mine
                            // Increment count by 1

                    if (desiredX >= 0 && desiredY >= 0 && desiredX < width && desiredY < height)
                    {
                        Tile tile = tiles[desiredX, desiredY];

                        if (tile.isMine)
                        {
                            count++;
                        }
                    }               
                }
            }
            

            return count;
        }

       public void FFuncover(int x, int y, bool[,] visited)
        {

            // If x >= 0 and y >= 0 and x < width AND y < height
            // If visited[x, y]
            // return
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                if (visited[x, y])
                {
                    return;
                }
            }

            // LET tile = tiles[x, y]
            // LET adjecentMines = GetAdjacentMineCountAt(tile)
            // CALL tile.Reveal(adjacentMines)
            Tile tile = tiles[x, y];
            int adjacentMines = GetAdjacentMineCountAt(tile);
            tile.Reveal(adjacentMines);

            // If adjacentMines > 0
                // return
            if (adjacentMines > 0)
            {
                return;
            }

            // SET visited[x,y] = true
            visited[x, y] = true;

            // CALL FFuncover(x - 1, y, visited)
            // CALL FFuncover(x + 1, y, visited)
            // CALL FFuncover(x, y - 1, visited)
            // CALL FFuncover(x, y + 1, visited)

            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }

        public void UncoverMines(int mineState)
        {
            // FOR x = 0 to x < width
                // FOR y = 0 to y < height
                    // LET currentTile = tile[x, y]
                    // If currentTile isMine
                        // LET adjacentMines = GetAdjacentMineCountAt(currentTile)
                        // CALL currentTile.Reveal(adjacentMines, mineState)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile currentTile = tiles[x, y];
                    if (currentTile.isMine)
                    {
                        int adjacentMines = GetAdjacentMineCountAt(currentTile);
                        currentTile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        // Detects if there are no more empty tiles in the game
        bool NoMoreEmptyTiles()
        {
            // LET emptyTileCount = 0
            int emptyTileCount = 0;

            // FOR x = 0 to x < width
                // FOR y = 0 to y < height
                    // LET currentTile = tiles[x, y]
                    // IF !currentTile.isRevealed AND !currentTile.isMine
                    // SET emptyTileCount = emptyTileCount + 1
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile currentTile = tiles[x, y];
                    if (!currentTile.isRevealed && !currentTile.isMine)
                    {
                        emptyTileCount++;
                    }
                }
            }

            // RETURN emptyTileCount
            return emptyTileCount == 0;
        }

        // Takes in a tile selected by the user in some way to reveal it
        public void SelectTile(Tile selectedTile)
        {
            // LET adjacentMines = GetAdjacentMineCountAt(selectedTile)
            // CALL selectedTile.Reveal(AdjacentMines)
            // IF selecedTile isMine
                // CALL UncoverMines(0)
                // [EXTRA] Perform game over logic
            // ELSEIF adjacentMines == 0
                // LET x = selectedTile.x
                // LET y = selectedTile.y
                // CALL FFuncover(x,y, new bool[width, height])
            // If NoMoreEmptyTiles()
                // CALL UncoverMines(1)
                // [EXTRA] Perform Win logic
        }

        // Use this for initialisation
        void Start()
        {
            GenerateTiles();
        }

        void Update()
        {
            // If Mouse Button 0 is down
                // LET ray = Ray from Camera using Input.mousePosition
                // LET hit = Physics2D RayCast(ray.origin, ray.direction)
                // IF hit's collider != null
                    // LET hitTile = hit collider's Tile component
                    // IF hitTile != null
                        // CALL SelectTile(hitTile)
        }

    }
}