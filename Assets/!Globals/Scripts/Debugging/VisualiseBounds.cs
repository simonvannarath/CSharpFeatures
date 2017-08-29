using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class VisualiseBounds : MonoBehaviour
{
    private Renderer rend;

    private void OnDrawGizmos()
    {
        rend = GetComponent<Renderer>();
        Gizmos.DrawWireCube(rend.bounds.center, rend.bounds.size);
    }
}
