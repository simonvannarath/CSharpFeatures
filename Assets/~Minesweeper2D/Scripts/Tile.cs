using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Tile : MonoBehaviour
    {
        // x, y coordinates in array 
        public int x = 0;
        public int y = 0;
        public bool isMine = false;     // Is current tile a mine?
        public bool isRevealed = false; // Has the tile already been revealed?

        [Header("References")]
        public Sprite[] emptySprites;   // List of empy sprites i.e. empty, 1, 2, 3, etc.
        public Sprite[] mineSprites;    // The mine sprites

        private SpriteRenderer rend;    // Reference to sprite renderer

        void Awake()
        {
            // Grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void Start()
        {
            // Randomly decide if it's mine or not
            isMine = Random.value < .05f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flags the tile as being revealed
            isRevealed = true;

            // Checks if tile is a mine
            if (isMine)
            {
                // Sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }
    }

}
