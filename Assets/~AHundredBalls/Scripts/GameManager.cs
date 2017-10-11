using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHundredBalls
{
    public class GameManager : MonoBehaviour
    {

        public int score;
        public float scrW, scrH;

        // Use this for initialization
        void Start()
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;
            score = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        // GUI Section
        private void OnGUI()
        {
            GUI.Box(new Rect(1 * scrW, 0.25f * scrH, 0.5f * scrW, 0.5f * scrH), score.ToString()); // score on top
        }

        public void IncrementScore(int amount) // Score increases per block type amount
        {
            score++;
        }
    }
}