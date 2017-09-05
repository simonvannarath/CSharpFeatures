﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recursion
{
    public class ScaleOnY : MonoBehaviour
    {

        public float maxScale = 100f;

        private float originalY = 0;
        private float percentY = 0;

        // Use this for initialization
        void Start()
        {
            originalY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 scale = transform.localScale;
            Vector3 position = transform.position;

            float yPercent = position.y / maxScale;
            float invertYPercent = 1 - yPercent;

            float scaleFactor = maxScale * invertYPercent;
            scale = Vector3.one * scaleFactor;

            transform.localScale = scale;
        }
    }
}
