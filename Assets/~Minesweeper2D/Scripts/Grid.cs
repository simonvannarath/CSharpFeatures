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

        public bool isMine = false;

        private Tile[,] tiles;

        
        Tile SpawnTile(Vector3 pos)                                                                 // Functionality for spawning tiles
        {
            
            GameObject clone = Instantiate(tilePrefab);                                             // Clone tile prefab
            clone.transform.position = pos;                                                         // Position tile
            Tile currentTile = clone.GetComponent<Tile>();                                          // Get Tile component
            return currentTile;                                                                     // return it
        }

        public void GenerateRand()
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

       
        void GenerateTiles()                                                                           //Spawns tiles in a grid-like pattern
        {
            tiles = new Tile[width, height];                                                           // Create new 2D array of size width x height
            
            for (int x = 0; x < width; x++)                                                            // Loop through the entire tile list
            {

                for (int y = 0; y < height; y++)
                {                    
                    Vector2 halfSize = new Vector2(width / 2.25f, height / 2.25f);                     // Store half size for later use                 
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);                         // Pivot tiles around Grid
                    pos *= spacing;                                                                    // Apply spacing
                    Tile tile = SpawnTile(pos);                                                        // Spawn the tile
                    tile.transform.SetParent(transform);                                               // Attach newly spawned tile to

                    // Store its array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;                   
                    tiles[x, y] = tile;                                                                // Store tile in array at those coordinates
                }
            }
        }

        /*void FixedUpdate()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);

                if (hit.collider != null)
                {   
                    Tile tile = hit.collider.GetComponent<Tile>();                                     // Let tile == hit collider's Tile component
 
                    if(tile != null)                                                                   // If tile =! null
                    {
                        int adjacentMines = GetAdjacentMineCountAt(tile);                              // Let adjacentmines = GetAdjacentMineCountAt
                        tile.Reveal(adjacentMines);                                                    // Call tile.Reveal(adjacentMines)
                    }
                }
            }   
        }
        */
        
        public int GetAdjacentMineCountAt(Tile t)                                                      // Count adjacent mines at element
        {
            int count = 0;

            for (int x = -1; x <= 1; x++)
            {
                
                for (int y = -1; y <= 1; y++)                                                          // Loop through all elements and have each axis go between -1 to 1
                {
                    // Calculate desired coordinates from ones attained
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;

                    if (desiredX >= 0 && desiredY >= 0 && desiredX < width && desiredY < height)       // If desiredX is within range of tiles array length
                    {
                        Tile tile = tiles[desiredX, desiredY]; 

                        if (tile.isMine)                                                               // If the element at index is a mine
                        {
                            count++;                                                                   // Increment count by 1
                        }
                    }               
                }
            }

            return count;
        }

       public void FFuncover(int x, int y, bool[,] visited)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)                                           // If x >= 0 and y >= 0 and x < width AND y < height
            {
                if (visited[x, y])                                                                     // If visited[x, y]
                {
                    return;                                                                            // return
                }
            }

            Tile tile = tiles[x, y];                                                                   // LET tile = tiles[x, y]
            int adjacentMines = GetAdjacentMineCountAt(tile);                                          // LET adjecentMines = GetAdjacentMineCountAt(tile)
            tile.Reveal(adjacentMines);                                                                // CALL tile.Reveal(adjacentMines)
                
            if (adjacentMines > 0)                                                                     // If adjacentMines > 0
            {
                return;                                                                                // return
            }

            visited[x, y] = true;                                                                      // SET visited[x,y] = true
            FFuncover(x - 1, y, visited);                                                              // CALL FFuncover(x - 1, y, visited)
            FFuncover(x + 1, y, visited);                                                              // CALL FFuncover(x + 1, y, visited)
            FFuncover(x, y - 1, visited);                                                              // CALL FFuncover(x, y - 1, visited)
            FFuncover(x, y + 1, visited);                                                              // CALL FFuncover(x, y + 1, visited)
        }

        public void UncoverMines(int mineState)
        {                       
            for (int x = 0; x < width; x++)                                                            // FOR x = 0 to x < width
            {
                for (int y = 0; y < height; y++)                                                       // FOR y = 0 to y < height
                {
                    Tile currentTile = tiles[x, y];                                                    // LET currentTile = tile[x, y]
                    if (currentTile.isMine)                                                            // If currentTile isMine
                    {
                        int adjacentMines = GetAdjacentMineCountAt(currentTile);                       // LET adjacentMines = GetAdjacentMineCountAt(currentTile)
                        currentTile.Reveal(adjacentMines, mineState);                                  // CALL currentTile.Reveal(adjacentMines, mineState)
                    }
                }
            }
        }

        bool NoMoreEmptyTiles()                                                                        // Detects if there are no more empty tiles in the game
        {
            int emptyTileCount = 0;                                                                    // LET emptyTileCount = 0
       
            for (int x = 0; x < width; x++)                                                            // FOR x = 0 to x < width
            {
                for (int y = 0; y < height; y++)                                                       // FOR y = 0 to y < height
                {
                    Tile currentTile = tiles[x, y];                                                    // LET currentTile = tiles[x, y]

                    if (!currentTile.isRevealed && !currentTile.isMine)                                // IF !currentTile.isRevealed AND !currentTile.isMine
                    {
                        emptyTileCount++;                                                              // SET emptyTileCount = emptyTileCount + 1
                    }
                }
            }

            return emptyTileCount == 0;                                                                // RETURN emptyTileCount
        }

        public void SelectTile(Tile selectedTile)                                                      // Takes in a tile selected by the user in some way to reveal it
        {
            
            int adjacentMines = GetAdjacentMineCountAt(selectedTile);                                  // LET adjacentMines = GetAdjacentMineCountAt(selectedTile)
            selectedTile.Reveal(adjacentMines);                                                        // CALL selectedTile.Reveal(adjacentMines)

            if(selectedTile.isMine)                                                                    // IF selecedTile isMine
            {
                UncoverMines((int)MineState.LOSS);                                                                       // CALL UncoverMines(0)
                // [EXTRA] Perform game over logic
            }

            else if (adjacentMines == 0)                                                               // ELSEIF adjacentMines == 0
            {
                int x = selectedTile.x;                                                                // LET x = selectedTile.x
                int y = selectedTile.y;                                                                // LET y = selectedTile.y
                FFuncover(x, y, new bool[width, height]);                                              // CALL FFuncover(x,y, new bool[width, height])
            }

            if (NoMoreEmptyTiles())                                                                    // If NoMoreEmptyTiles()
            {
                UncoverMines((int)MineState.WIN);                                                                     // CALL UncoverMines(1)
                // [EXTRA] Perform Win logic
            }
        }

        // Use this for initialisation
        void Start()
        {
            GenerateTiles();
            //isMine = 
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))                                            // If Mouse Button 0 is down
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            // LET ray = Ray from Camera using Input.mousePosition
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);        // LET hit = Physics2D RayCast(ray.origin, ray.direction)

                if (hit.collider != null)                                               // IF hit's collider != null
                {
                    Tile hitTile = hit.collider.GetComponent<Tile>();                   // LET hitTile = hit collider's Tile component

                    if (hitTile != null)                                                // IF hitTile != null
                    {
                        SelectTile(hitTile);                                            // CALL SelectTile(hitTile)
                    }
                }
            }
        }

    }
}